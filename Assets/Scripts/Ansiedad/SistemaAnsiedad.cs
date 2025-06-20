using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaAnsiedad : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    [Header("Valores")]
    public float ansiedad;
    public float ansiedadMaxima;

    [Header("Interfaz")]
    public Image BarraAnsiedad;
    public CanvasGroup Overlay;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clip;
    }

    private void Update()
    {

        ActualizarInterfaz();

        if (ansiedad < 0)
        { 
            ansiedad = 0;
        }
        if (ansiedad > ansiedadMaxima)
        {
            ansiedad = ansiedadMaxima;
        }

        if (ansiedad > 2200 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (ansiedad < 2300)
        {
            Overlay.alpha = 0f;
            audioSource.Stop();
        }

        if (ansiedad > 2300 && ansiedad < 2549)
        {
            Overlay.alpha = 0.7f;
        }

        if (ansiedad > 2500)
        {
            Overlay.alpha = 1f;
        }

        if (ansiedad >= ansiedadMaxima)
        {
            SceneManager.LoadScene("MenuDerrota");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
    public void RecibirAnsiedad(float cargaAnsiedad)
    {
        ansiedad += cargaAnsiedad;
    }
    public void BajarAnsiedad(float descargaAnsiedad)
    {
        ansiedad -= descargaAnsiedad;
    }
    void ActualizarInterfaz()
    {
        BarraAnsiedad.fillAmount = ansiedad / ansiedadMaxima;
    }
}
