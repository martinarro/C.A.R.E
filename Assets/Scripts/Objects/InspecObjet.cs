using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InspecObjet : MonoBehaviour
{
    [SerializeField] public GameObject objectInspect;
    [SerializeField] public GameObject detailObject;
    [SerializeField] public InventoryManager inventoryManager;

    [SerializeField] public DetailObject detailObjectText;
    public bool isCounted = false;
    void Start()
    {
        objectInspect.SetActive(false);
        detailObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectInspect.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                detailObject.SetActive(true);
                objectInspect.SetActive(false);
                if (isCounted == false)
                {
                    inventoryManager.AddObject(gameObject);
                    Debug.Log(inventoryManager.countObject);
                    isCounted = true;

                }
                
            }
            
             if (Input.GetKey(KeyCode.C))
                {
                    detailObject.SetActive(false);
                    objectInspect.SetActive(true);
                }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectInspect.SetActive(false);
            detailObject.SetActive(false);
            Debug.Log("Salgo de la colision con el player");
        }
    }
}
