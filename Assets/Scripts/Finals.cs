using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finals : MonoBehaviour
{
    public InventoryManager inventoryManager;


    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && (inventoryManager.countObject >= 4))
        {
            SceneManager.LoadScene("MenuFinPrototipo");
        }
        
    }


    void OnTriggerEnter(Collider other)
    {
        
    }
}
