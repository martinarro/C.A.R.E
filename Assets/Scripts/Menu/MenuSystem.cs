using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("MenuCarga");
    }

    public void Controles()
    {
        SceneManager.LoadScene("MenuControles");
    }

    public void BotonMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Salir()
    {
        Application.Quit();
    }

}
