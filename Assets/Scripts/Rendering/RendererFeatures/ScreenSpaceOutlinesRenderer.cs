using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenSpaceOutlinesRenderer : ScriptableRendererFeature
{
    [System.Serializable]
    private class ScreenSpaceOutlineSettings
    {
        [Header("Mask")]
        public LayerMask outlinesLayerMask = -1;
        public LayerMask zTestOcclusionMask = -1;

        [Header("View Space Normals Settings")]
        public string namePass = "Custom Pass: View Space Normals";
        public int depthBufferBits = 32;
        public RenderTextureFormat RTColorFormat = RenderTextureFormat.ARGB32;

        [Header("Outline Settings")]
        public string outlinePassName = "Custom Pass: Outlines";
        public Color outlineColor = Color.black;
        [Range(0.0f, 10.0f)]
        public float outlineSize = 1.0f;

        [Header("Depth/Normals Settings")]
        public float depthThreshold = 1.5f;
        public float robertsCrossMultiplier = 100.0f;
        public float normalThreshold = 0.4f;
        public float stepAngleThreshold = 0.2f;
        public float stepAngleMultiplier = 25.0f;

    }

    private class ViewSpaceNormalsTexturePass : ScriptableRenderPass
    {
        private readonly ScreenSpaceOutlineSettings settings;
        private readonly RTHandle m_Normals;
        private readonly string m_ProfilerTag;

        private readonly Material normalsMaterial;
        private readonly Material zTestOcclusionMaterial;

        private readonly int normalsBufferID;

        private RenderTextureDescriptor normalsTextureDescriptor;

        private FilteringSettings filteringSettings;
        private FilteringSettings zTestOcclusionFiltering;

        private ShaderTagId m_ShaderTagId = new ShaderTagId("DepthOnly");

        public ViewSpaceNormalsTexturePass(RenderPassEvent renderPassEvent, ScreenSpaceOutlineSettings settings, LayerMask outlinesLayerMask, LayerMask zTestOcclusionMask, Material normalsM, Material zTestM)
        {
            this.renderPassEvent = renderPassEvent;
            this.settings = settings;
            m_ProfilerTag = settings.namePass;

            m_Normals = RTHandles.Alloc("_SceneViewSpaceNormals", name: "_SceneViewSpaceNormals");

            normalsBufferID = Shader.PropertyToID(m_Normals.name);

            normalsMaterial = normalsM;
            zTestOcclusionMaterial = zTestM;

            filteringSettings = new FilteringSettings(RenderQueueRange.opaque, outlinesLayerMask);
            zTestOcclusionFiltering = new FilteringSettings(RenderQueueRange.opaque, zTestOcclusionMask);
        }

        public void Setup(RenderTextureDescriptor descriptor, RTHandle cameraColorTarget, RTHandle destinationDepth)
        {
            normalsTextureDescriptor = descriptor;
            normalsTextureDescriptor.colorFormat = settings.RTColorFormat;
            normalsTextureDescriptor.depthBufferBits = settings.depthBufferBits;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            cmd.GetTemporaryRT(normalsBufferID, normalsTextureDescriptor, FilterMode.Point);

            ConfigureTarget(m_Normals);
            ConfigureClear(ClearFlag.All, Color.black);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);

            using (new ProfilingScope(cmd, new ProfilingSampler(m_ProfilerTag)))
            {
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                DrawingSettings drawSettings = CreateDrawingSettings(m_ShaderTagId, ref renderingData, renderingData.cameraData.defaultOpaqueSortFlags);
                drawSettings.perObjectData = PerObjectData.None;
                drawSettings.overrideMaterial = normalsMaterial;

                DrawingSettings drawZTestOcclusionSettings = drawSettings;
                drawZTestOcclusionSettings.overrideMaterial = zTestOcclusionMaterial;

                context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filteringSettings);
                context.DrawRenderers(renderingData.cullResults, ref drawZTestOcclusionSettings, ref zTestOcclusionFiltering);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(Shader.PropertyToID(m_Normals.name));
        }

        public void Dispose()
        {
            m_Normals?.Release();

            // #if UNITY_EDITOR
            //             DestroyImmediate(normalsMaterial);
            //             DestroyImmediate(zTestOcclusionMaterial);
            // #else
            //             CoreUtils.Destroy(normalsMaterial);
            //             CoreUtils.Destroy(zTestOcclusionMaterial);
            // #endif
        }
    }

    private class ScreenSpaceOutlinePass : ScriptableRenderPass
    {
        private readonly Material m_OutlinesMaterial;
        private readonly string m_ProfilerTag;

        private RTHandle cameraColorTarget;
        private RTHandle tempBuffer;

        private readonly int tempBufferID = Shader.PropertyToID("_TemporaryBuffer");

        private readonly int outlineColor = Shader.PropertyToID("_OutlineColor");
        private readonly int outlineScale = Shader.PropertyToID("_OutlineScale");
        private readonly int robertsCrossMultiplier = Shader.PropertyToID("_RobertsCrossMultiplier");
        private readonly int depthThreshold = Shader.PropertyToID("_DepthThreshold");
        private readonly int normalThreshold = Shader.PropertyToID("_NormalThreshold");
        private readonly int stepAngleThreshold = Shader.PropertyToID("_StepAngleThreshold");
        private readonly int stepAngleMultiplier = Shader.PropertyToID("_StepAngleMultiplier");

        public ScreenSpaceOutlinePass(RenderPassEvent renderPassEvent, ScreenSpaceOutlineSettings settings, Material outlinesMaterial)
        {
            this.renderPassEvent = renderPassEvent;
            m_ProfilerTag = settings.outlinePassName;
            m_OutlinesMaterial = outlinesMaterial;
            m_OutlinesMaterial.SetColor(outlineColor, settings.outlineColor);
            m_OutlinesMaterial.SetFloat(outlineScale, settings.outlineSize);
            m_OutlinesMaterial.SetFloat(robertsCrossMultiplier, settings.robertsCrossMultiplier);
            m_OutlinesMaterial.SetFloat(depthThreshold, settings.depthThreshold);
            m_OutlinesMaterial.SetFloat(normalThreshold, settings.normalThreshold);
            m_OutlinesMaterial.SetFloat(stepAngleThreshold, settings.stepAngleThreshold);
            m_OutlinesMaterial.SetFloat(stepAngleMultiplier, settings.stepAngleMultiplier);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            RenderTextureDescriptor temporaryDescriptor = renderingData.cameraData.cameraTargetDescriptor;
            temporaryDescriptor.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempBufferID, temporaryDescriptor, FilterMode.Bilinear);

            RenderingUtils.ReAllocateIfNeeded(ref tempBuffer, temporaryDescriptor, FilterMode.Bilinear, name: "_TemporaryBuffer");

            cameraColorTarget = renderingData.cameraData.renderer.cameraColorTargetHandle;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler(m_ProfilerTag)))
            {
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                Blitter.BlitCameraTexture(cmd, cameraColorTarget, tempBuffer);
                Blitter.BlitCameraTexture(cmd, tempBuffer, cameraColorTarget, m_OutlinesMaterial, 0);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            cameraColorTarget = null;
            tempBuffer?.Release();
        }

        public void Dispose()
        {
            tempBuffer?.Release();
            // #if UNITY_EDITOR
            //             DestroyImmediate(m_OutlinesMaterial);
            // #else
            //             CoreUtils.Destroy(m_OutlinesMaterial);
            // #endif
        }
    }

    [SerializeField]
    private RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

    [SerializeField]
    private ScreenSpaceOutlineSettings settings = new();

    private ViewSpaceNormalsTexturePass viewSpaceNormalsTexturePass;
    private ScreenSpaceOutlinePass viewSpaceOutlinePass;

    private Material normalsMaterial;
    private Material zTestOcclusionMaterial;
    private Material outlinesMaterial;

    public override void Create()
    {
        normalsMaterial = new Material(Shader.Find("Shader Graphs/ViewSpaceNormalsShader"));
        zTestOcclusionMaterial = new Material(Shader.Find("Shader Graphs/ZTestOcclusionShader"));

        outlinesMaterial = new Material(Shader.Find("Shader Graphs/OutlinesShader"));

        if (!normalsMaterial || !zTestOcclusionMaterial || !outlinesMaterial)
        {
            return;
        }

        viewSpaceNormalsTexturePass = new ViewSpaceNormalsTexturePass(renderPassEvent, settings, settings.outlinesLayerMask, settings.zTestOcclusionMask, normalsMaterial, zTestOcclusionMaterial);
        viewSpaceOutlinePass = new ScreenSpaceOutlinePass(renderPassEvent, settings, outlinesMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType != CameraType.Game)
        {
            return;
        }

        if (!outlinesMaterial)
        {
            return;
        }

        renderer.EnqueuePass(viewSpaceNormalsTexturePass);
        renderer.EnqueuePass(viewSpaceOutlinePass);
    }

    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        viewSpaceNormalsTexturePass.Setup(renderingData.cameraData.cameraTargetDescriptor, renderer.cameraColorTargetHandle, renderer.cameraDepthTargetHandle);
    }

    protected override void Dispose(bool disposing)
    {
#if UNITY_EDITOR
        DestroyImmediate(normalsMaterial);
        DestroyImmediate(zTestOcclusionMaterial);
        DestroyImmediate(outlinesMaterial);
#else
        CoreUtils.Destroy(normalsMaterial);
        CoreUtils.Destroy(zTestOcclusionMaterial);
        CoreUtils.Destroy(outlinesMaterial);
#endif
        viewSpaceNormalsTexturePass.Dispose();
        viewSpaceOutlinePass.Dispose();
    }
}
