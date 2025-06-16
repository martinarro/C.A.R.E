using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesLanzadores : MonoBehaviour
{
   [SerializeField] public GameObject spawn;
    [SerializeField] public GameObject enemie;
    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;

    //PARAMETROS PARA UI
    [SerializeField] public GameObject objectInspect;
    [SerializeField] public GameObject detailObject;
    [SerializeField] public InventoryManager inventoryManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject enemyInstance = Instantiate(enemie, spawn.transform.position, spawn.transform.rotation);
            enemyInstance.GetComponent<NavMeshShadow>().pointA = pointA;
            enemyInstance.GetComponent<NavMeshShadow>().pointB = pointB;

            LanzarObjeto lanzar = enemyInstance.GetComponent<LanzarObjeto>();
            lanzar.objectInspect = objectInspect;
            lanzar.detailObject = detailObject;
            lanzar.inventoryManager = inventoryManager;

            Destroy(gameObject);
        }
    }
}
