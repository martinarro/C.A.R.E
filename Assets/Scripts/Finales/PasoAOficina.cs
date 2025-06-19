using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasoAOficina : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public int minObject = 0;
    public GameObject faltanElementos;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && inventoryManager.countObject < minObject)
        {
            faltanElementos.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Player") && inventoryManager.countObject >= minObject)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
            {
                faltanElementos.SetActive(false);
            }
    }
     
}
