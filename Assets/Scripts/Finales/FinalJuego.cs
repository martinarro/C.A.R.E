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
                inventoryManager.Final();
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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            faltanElementos.SetActive(false);
        }
    }

}
