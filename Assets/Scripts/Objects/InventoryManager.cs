using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;
using System;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] public TextMeshProUGUI countText;
    [SerializeField] public TextMeshProUGUI detailObjectsTexts;
    [SerializeField] public GameObject listInventory;

    public int itemsA = 0;
    public int itemsB = 0;
    public bool contrasenia = false;

    public string ultimoItem;

    public bool isInventoryOpen = false;
    public int countObject = 0;
    void Start()
    {

    }

    void Update()
    {
        ShowInventory();
    }

    public void AddObject(GameObject objectFinded)
    {
        countObject++;
        countText.text = "" + countObject;
        inventory.Add(objectFinded);
    }

    public void ShowInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            listInventory.SetActive(isInventoryOpen);

            if (isInventoryOpen)
            {
                detailObjectsTexts.text = "";
                foreach (GameObject obj in inventory)
                {
                    DetailObject detalle = obj.GetComponent<DetailObject>();
                    Debug.Log(detalle.detailObject);

                    detailObjectsTexts.text += "• " + detalle.detailObject + "\n";
                }

            }
        }
    }
    
    public bool TieneContrasenia()
{
    foreach (GameObject obj in inventory)
    {
        DetailObject detalle = obj.GetComponent<DetailObject>();
        if (detalle.detailObject == "Contraseña")
        {
            contrasenia = true;
            return true;
        }
    }

    contrasenia = false;
    return false;
}

    public void Final()
    {
        foreach (GameObject obj in inventory)
        {
            DetailObject typeFinal = obj.GetComponent<DetailObject>();


            if (typeFinal.typeFinal == "A")
            {
                Debug.Log("SUMO A");
                itemsA++;
                ultimoItem = "A";
            }
            else if (typeFinal.typeFinal == "B")
            {
                itemsB++;
                ultimoItem = "B";
            }
            else if (typeFinal.typeFinal == "AB")
            {
                itemsA++;
                itemsB++;
                Debug.Log("SUME AMBOS");
            }



        }

        if (itemsA > itemsB)
        {
            Debug.Log("CINEMATICA AAAAAAAAAAA");
            SceneManager.LoadScene("FinalA");
        }
        else if (itemsA == itemsB)
        {
            if (ultimoItem == "A")
            {
                Debug.Log("CINEMATICA AAAAAAAAAAA");
                SceneManager.LoadScene("FinalA");
            }
            if (ultimoItem == "B")
            {
                Debug.Log("CINEMATICA BBBBBBBBBBBB");
                //SceneManager.LoadScene("FinalB");        
            }
        }

        if (itemsB > itemsA)
        {
            Debug.Log("CINEMATICA BBBBBBBBBBBB");
            SceneManager.LoadScene("FinalB");
        }

        Debug.Log("Items A: " + itemsA);
        Debug.Log("Items B: " + itemsB);
    }
        
    
}
