using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finals : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject faltanElementos;
    public int minObject = 4;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (inventoryManager.countObject >= minObject))
        {
            Debug.Log("");
            //SceneManager.LoadScene("MenuFinPrototipo");
        }
        else if (other.gameObject.CompareTag("Player") && (inventoryManager.countObject < minObject))
        {
            //faltanElementos.SetActive(true);
            inventoryManager.Final();
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
