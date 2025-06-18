using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SistemaAnsiedad : MonoBehaviour
{

    [Header("Valores")]
    public float ansiedad;
    public float ansiedadMaxima;

    [Header("Interfaz")]
    public Image BarraAnsiedad;
    public CanvasGroup Overlay;

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

        if (ansiedad < 1000)
        {
            Overlay.alpha = 0f;
        }

        if (ansiedad > 1000 && ansiedad < 1249)
        {
            Overlay.alpha = 0.7f;
        }

        if (ansiedad > 1250)
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
