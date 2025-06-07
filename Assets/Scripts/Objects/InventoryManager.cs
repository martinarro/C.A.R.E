using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] public TextMeshProUGUI countText;
    [SerializeField] public TextMeshProUGUI detailObjectsTexts;
    [SerializeField] public GameObject listInventory;

    public bool isInventoryOpen = false;
    public int countObject = 0;
    void Start()
    {

    }

    void Update()
    {
        ShowInventory();

        if (countObject >= 4)
        {
            SceneManager.LoadScene("MenuFinPrototipo");
        }
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
}
