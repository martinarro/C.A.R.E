using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarAnsiedadAPlayer : MonoBehaviour
{
    public Transform posicionPlayer;
    public float distanciaParaDanio = 2f;
    public SistemaAnsiedad sistemaAnsiedad;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        posicionPlayer = player.transform;
        sistemaAnsiedad = player.GetComponent<SistemaAnsiedad>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, posicionPlayer.position);

        if (distancia <= distanciaParaDanio)
        {
            //sistemaAnsiedad.RecibirAnsiedad(1000);
            Debug.Log("LE HAGO DAÑO");
        }
    }
}
