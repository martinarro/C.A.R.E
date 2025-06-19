using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruirBrillo : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
