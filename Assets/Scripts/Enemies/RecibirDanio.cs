using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecibirDanio : MonoBehaviour
{
    public Image barraRelleno; 
    public float tiempoVida = 3f;

    private float tiempoActual = 0f;
    private bool estaIluminado = false;

    public GameObject efectoMuerte;

    void Update()
    {
        if (estaIluminado)
        {
            tiempoActual += Time.deltaTime;

            if (tiempoActual >= tiempoVida)
            {
                Morir();
            }
        }
        else
        {
            // Vaciar cuando no se lo ataca
            tiempoActual -= Time.deltaTime;
            tiempoActual = Mathf.Clamp(tiempoActual, 0f, tiempoVida);
        }

        if (barraRelleno != null)
            barraRelleno.fillAmount = 1f - tiempoActual / tiempoVida;

        estaIluminado = false; 
    }

    public void RecibirLuz()
    {
        estaIluminado = true;
    }

    void Morir()
    {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
