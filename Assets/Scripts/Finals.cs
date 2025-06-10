using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finals : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public int minObject = 4;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && (inventoryManager.countObject >= minObject))
        {
            Debug.Log("Entre al if");
            SceneManager.LoadScene("MenuFinPrototipo");
        }
    }

}
