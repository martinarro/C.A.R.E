using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LuzDetector : MonoBehaviour
{
    public int rango = 8;
    public LinternaScript linternaScript;

    void Update()
    {
        if (!linternaScript.luzLinterna.enabled)
            return;

        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        Debug.DrawRay(ray.origin, ray.direction * rango, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rango, ~0))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("Enemigo"))
            {
                Destroy(hit.collider.gameObject, 1);
            }
                
            
        }
    }
}