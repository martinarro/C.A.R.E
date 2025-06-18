using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarAnsiedadAPlayer : MonoBehaviour
{
    public Transform posicionPlayer;
    public float distanciaParaDanio = 2f;
    public float cantidadAnsiedad;

    public SistemaAnsiedad sistemaAnsiedadJugador;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
       
        if (player != null)
        {
            Debug.Log("Obtuve al player");
        }

        posicionPlayer = player.transform;
        sistemaAnsiedadJugador = FindObjectOfType<SistemaAnsiedad>();



        if (sistemaAnsiedadJugador != null)
        {
            Debug.Log("Obtuve sistema de ansiedad");
        }
        else
        {
            Debug.Log("No obtuve el sistema de ansiedad");
        }
        
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, posicionPlayer.position);

        if (distancia <= distanciaParaDanio)
        {
            sistemaAnsiedadJugador.ansiedad += 500 * Time.deltaTime; //Con esto se regula la cantidad de anisedad que el enemigo causa.
            Debug.Log("LE HAGO DAÑO");
        }
    }
}