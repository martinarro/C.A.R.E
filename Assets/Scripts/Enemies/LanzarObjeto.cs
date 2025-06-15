using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarObjeto : MonoBehaviour
{
    public Transform posicionPlayer;
    public float distanciaParaDanio = 2f;
    public GameObject objetoAInspeccionar;
    public bool fueIntanciado = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        posicionPlayer = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, posicionPlayer.position);

        if (distancia <= distanciaParaDanio && fueIntanciado == false)
        {
           // Instantiate(objetoAInspeccionar, transform.position, transform.rotation);
            fueIntanciado = true;
            Debug.Log("LANZO OBJETO");
        }
    }

}
