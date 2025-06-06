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
