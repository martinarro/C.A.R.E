using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject menuPausa; 
    private bool estaPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estaPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f; // Pausa
        menuPausa.SetActive(true); 
        estaPausado = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1f; // Vuelve a la normalidad
        menuPausa.SetActive(false); 
        estaPausado = false;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }
}

