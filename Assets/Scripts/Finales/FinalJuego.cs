using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalJuego : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject faltanElementos;
    public int minObject;
    public GameObject player;
    public GameObject mensajeReproducir;
    public bool puedeReproducir = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre en colision");
        Debug.Log("CountObject: " + inventoryManager.countObject + " / MinObject: " + minObject);
        Debug.Log("Contrasenia: " + inventoryManager.contrasenia);


        if (other.gameObject.CompareTag("Player") && inventoryManager.countObject >= minObject)
        {
            Debug.Log("Entre al if del final");
            inventoryManager.TieneContrasenia();
            if (inventoryManager.TieneContrasenia())
            {
                mensajeReproducir.SetActive(true);
                puedeReproducir = true;
            }
        }
        else
        {
            Debug.Log("Hay algo que no se cumple");
        }


    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.C))
        {
            faltanElementos.SetActive(false);
        }

         if (puedeReproducir && Input.GetKeyDown(KeyCode.R))
        {
            inventoryManager.Final();
            puedeReproducir = false; 
        }
        
        
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            faltanElementos.SetActive(false);
            mensajeReproducir.SetActive(false);
            puedeReproducir = false;
        }
        
    }

}
