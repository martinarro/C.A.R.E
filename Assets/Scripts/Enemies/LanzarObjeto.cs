using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarObjeto : MonoBehaviour
{
    public Transform posicionPlayer;
    public float distanciaParaDanio = 2f;
    public GameObject objetoAInspeccionar;
    public bool fueInstanciado = false;

    //PARAMETROS PARA UI
    [SerializeField] public GameObject objectInspect;
    [SerializeField] public GameObject detailObject;
    [SerializeField] public InventoryManager inventoryManager;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        posicionPlayer = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, posicionPlayer.position);

        if (distancia <= distanciaParaDanio && fueInstanciado == false)
        {
            GameObject objeto = Instantiate(objetoAInspeccionar, transform.position, transform.rotation);

            InspecObjet script = objeto.GetComponent<InspecObjet>();
            script.objectInspect = objectInspect;
            script.detailObject = detailObject;
            script.inventoryManager = inventoryManager;

            fueInstanciado = true;            
            Debug.Log("LANZO OBJETO");
        }
    }

}
