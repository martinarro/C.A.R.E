using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAudio : MonoBehaviour
{
    public float tiempoEntreReproduccion = 4f;
    public float time;
    public AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= tiempoEntreReproduccion)
        {
            audioSource.Play();
            Debug.Log("Reproducir audio");
            time = 0f;
        }
        
    }
}
