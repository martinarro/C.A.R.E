using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaDestructora : MonoBehaviour
{
    public GameObject[] imagenes;      // Asign� las im�genes en el Inspector
    public float intervalo = 2f;       // Tiempo entre destrucci�n (segundos)

    private int indice = 0;
    private float tiempo = 0f;

    void Update()
    {
        if (indice >= imagenes.Length) return;

        tiempo += Time.deltaTime;

        if (tiempo >= intervalo)
        {
            Destroy(imagenes[indice]);
            indice++;
            tiempo = 0f;
        }
    }
}
