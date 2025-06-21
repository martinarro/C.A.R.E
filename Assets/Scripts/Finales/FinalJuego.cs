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
    public GameObject mensajeFaltaContraseña;
    public bool puedeReproducir = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entre en colision");
        Debug.Log("CountObject: " + inventoryManager.countObject + " / MinObject: " + minObject);
        Debug.Log("Contrasenia: " + inventoryManager.contrasenia);


        if (other.gameObject.CompareTag("Player") && inventoryManager.countObject >= minObject)
        {
            Debug.Log("Entre al if del final");
            puedeReproducir = inventoryManager.TieneContrasenia();
            if (puedeReproducir)
            {
                mensajeReproducir.SetActive(true);
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

        if (puedeReproducir == false && Input.GetKeyDown(KeyCode.R))
        {
            mensajeFaltaContraseña.SetActive(true);

        }

         if (Input.GetKeyDown(KeyCode.C))
            {
                mensajeFaltaContraseña.SetActive(false);
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
