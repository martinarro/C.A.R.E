using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] public GameObject spawn;
    [SerializeField] public GameObject enemie;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(enemie, spawn.transform.position, spawn.transform.rotation);
            Destroy(gameObject);
        }
    }
}
