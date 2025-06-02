using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] public GameObject spawn;
    [SerializeField] public GameObject enemie;
    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject enemyInstance = Instantiate(enemie, spawn.transform.position, spawn.transform.rotation);
            enemyInstance.GetComponent<NavMeshShadow>().pointA = pointA;
            enemyInstance.GetComponent<NavMeshShadow>().pointB = pointB;
            Destroy(gameObject);
        }
    }
}
