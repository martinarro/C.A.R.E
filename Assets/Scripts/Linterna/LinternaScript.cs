using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinternaScript : MonoBehaviour
{
    public Light luzLinterna;

    [Header("Bateria")]
    public float energiaActual = 100;
    public float energiaMaxima = 100;
    public float velocidadConsumo;
    public float velocidadRecarga;

    [Header("Interfaz")]
    public Image barraBateria;

    [Header("Parpadeo")]
    public float umbralParpadeo;
    public float duracionParpadeo;
    public float frecuenciaParpadeo;

    private bool estaParpadeando = false;

    void Update()
    {
        //Input de teclado
        if (Input.GetButtonDown("Linterna"))
        {
            if (luzLinterna.enabled)
            {
                luzLinterna.enabled = false;
            }
            else if (!luzLinterna.enabled && energiaActual >= 10)
            {
                luzLinterna.enabled = true;
            }
        }

        //Define la velocidad de consumo y comienza la corrutina de parpadeo
        if (luzLinterna.enabled)
        {
            energiaActual -= Time.deltaTime * velocidadConsumo;

            if (energiaActual <= umbralParpadeo && !estaParpadeando)
            {
                StartCoroutine(ParpadearAntesDeApagar());
            }

            if (energiaActual <= 0)
            {
                energiaActual = 0;
                luzLinterna.enabled = false;
                estaParpadeando = false;
            }
        }
        else
        {
            energiaActual += Time.deltaTime * velocidadRecarga;
            if (energiaActual >= energiaMaxima)
            {
                energiaActual = energiaMaxima;
            }
        }

        //Cambia el color a rojo cuando queda poca bateria
        if (energiaActual <= 10)
        {
            Color colorOriginal = barraBateria.color;
            barraBateria.color = Color.red;
        }
        else if (energiaActual >= 10)
        {
            barraBateria.color = Color.green;
        }

        barraBateria.fillAmount = energiaActual / energiaMaxima;
    }

    //Corrutina para el parpadeo de la linterna y la barra
    IEnumerator ParpadearAntesDeApagar()
    {
        estaParpadeando = true;

        float tiempoRestante = duracionParpadeo;
        Color colorOriginal = barraBateria.color;
        barraBateria.color = Color.red;

        while (tiempoRestante > 0 && energiaActual > 0)
        {
            luzLinterna.enabled = !luzLinterna.enabled;
            barraBateria.enabled = !barraBateria.enabled;

            yield return new WaitForSeconds(frecuenciaParpadeo);
            tiempoRestante -= frecuenciaParpadeo;
        }

        luzLinterna.enabled = false;
        barraBateria.enabled = true;
        barraBateria.color = colorOriginal;

        estaParpadeando = false;
    }
}