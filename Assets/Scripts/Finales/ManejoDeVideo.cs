using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejoDeVideo : MonoBehaviour
{
    public float time = 0f;
    public float timeForPlay = 5;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Debug.Log(time);
        if (time >= timeForPlay)
        {
            SceneManager.LoadScene("Creditos");
        }    
    }
}
