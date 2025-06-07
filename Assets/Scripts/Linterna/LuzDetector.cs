using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzDetector : MonoBehaviour
{
    public int rango = 8;
    public LinternaScript linternaScript; // ← Se asigna desde el Inspector

    void Update()
    {
        if (!linternaScript.luzLinterna.enabled)
            return;

        Debug.DrawRay(transform.position, transform.forward * rango, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rango, ~0))
        {
            Debug.Log(hit.collider.name);

            NavMeshShadow enemigo = hit.collider.GetComponentInParent<NavMeshShadow>();
            if (enemigo != null)
            {
                Debug.Log("ENEMIGO");
                enemigo.enabled = false;
                Destroy(enemigo.gameObject, 1);
            }
        }
    }
}