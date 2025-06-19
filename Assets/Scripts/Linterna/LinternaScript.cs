using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinternaScript : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public Light luzLinterna;

    [Header("Ansiedad")]
    public SistemaAnsiedad sistemaAnsiedad;
    public float cargaAnsiedad;
    public float descargaAnsiedad;

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

    /*/ATAQUE
    public GameObject luzDetector;
    public int rango = 8;
    */

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clip;
    }

    void Update()
    {
        
        //Recibir y bajar la ansiedad, toma la funcion desde el script SistemaAnsiedad
        if (!luzLinterna.enabled)
        {
            sistemaAnsiedad.RecibirAnsiedad(cargaAnsiedad * Time.deltaTime);
        }
        else
        {
            sistemaAnsiedad.BajarAnsiedad(descargaAnsiedad * Time.deltaTime);
        }

        //Input de teclado
        if (Input.GetMouseButtonDown(0))
        {
            // Reproducir una vez
            audioSource.Play();

            if (luzLinterna.enabled)
            {
                luzLinterna.enabled = false;
//                luzDetector.SetActive(false);
            }
            else if (!luzLinterna.enabled && energiaActual >= 10)
            {
                luzLinterna.enabled = true;
  //              luzDetector.SetActive(true);
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

        //Cambia el alpha cuando queda poca bateria
        if (energiaActual <= 15)
        {
            Color colorActual = barraBateria.color;
            colorActual.a = 0.1f; // Valor entre 0 y 1
            barraBateria.color = colorActual;
        }
        else if (energiaActual >= 15)
        {
            Color colorActual = barraBateria.color;
            colorActual.a = 1f; // Valor entre 0 y 1
            barraBateria.color = colorActual;
        }

        barraBateria.fillAmount = energiaActual / energiaMaxima;
    }

    //Corrutina para el parpadeo de la linterna y la barra
    IEnumerator ParpadearAntesDeApagar()
    {
        estaParpadeando = true;

        float tiempoRestante = duracionParpadeo;

        while (tiempoRestante > 0 && energiaActual > 0)
        {
            luzLinterna.enabled = !luzLinterna.enabled;
            barraBateria.enabled = !barraBateria.enabled;

            yield return new WaitForSeconds(frecuenciaParpadeo);
            tiempoRestante -= frecuenciaParpadeo;
        }

        luzLinterna.enabled = false;
        barraBateria.enabled = true;
        estaParpadeando = false;
    }
}