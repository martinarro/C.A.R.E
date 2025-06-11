using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnObservador : MonoBehaviour
{
    public GameObject observadorEnemy;
    public Transform spawn;

    public bool isSpawned = false; // Bool para spawnear una sola vez al enemigo
    public float timeToSpawn = 5f; // Tiempo necesario para spawnear
    public float timeSpawn = 0f; // Temporizador para spawn

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeSpawn += Time.deltaTime;
            if (timeSpawn >= timeToSpawn && isSpawned == false)
            {
                isSpawned = true;
                Instantiate(observadorEnemy, spawn.position, Quaternion.identity);
                Debug.Log("SPAWNEO OBSERVADOR");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeSpawn = 0;
            Debug.Log("Salgo del spawn" + timeSpawn);
        }
    }
}
