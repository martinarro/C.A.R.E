using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaAnsiedad : MonoBehaviour
{
    [Header("Valores")]
    public float ansiedad = 0;
    public float ansiedadMaxima = 10000;

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

        if (ansiedad < 500)
        {
            Overlay.alpha = 0f;
        }

        if (ansiedad > 500 && ansiedad < 999)
        {
            Overlay.alpha = 0.5f;
        }

        if (ansiedad > 1000)
        {
            Overlay.alpha = 1f;
        }

        if (ansiedad >= ansiedadMaxima)
        { 
            //HACER MUERTE ACA, NO TOCAR
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
