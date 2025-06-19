using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finals : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public GameObject faltanElementos;
    public int minObject = 4;
    public GameObject player;
    public Transform spawnANivl2;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && inventoryManager.countObject >= minObject)
        {

            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; // Desactiva antes de mover
            }

            player.transform.position = spawnANivl2.position;

            if (cc != null)
            {
                cc.enabled = true; // Reactiva despues de mover
            }

            GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
                foreach (GameObject enemigo in enemigos)
                {
                    Destroy(enemigo);
                }
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
