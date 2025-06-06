using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzDetector : MonoBehaviour
{
    public int rango = 8;

    void Update()
    {   
        Debug.DrawRay(transform.position, transform.right * rango, Color.blue);  

        // Ataque con linterna
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, rango))
        {
            NavMeshShadow enemigo = hit.collider.GetComponentInParent<NavMeshShadow>();
            if (enemigo)
            {
                Debug.Log("LE PEGUE");
                Debug.Log(hit.collider.name);
                enemigo.enabled = false;
                Destroy(enemigo.gameObject, 1);
            }
        }
    }
}