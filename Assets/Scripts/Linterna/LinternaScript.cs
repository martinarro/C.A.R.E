using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternaScript : MonoBehaviour
{
    public Light luzLinterna;
    void Update()
    {
        if (Input.GetButtonDown("Linterna"))
        {
            if (luzLinterna.enabled)
            {
                luzLinterna.enabled = false;
            }
            else if (!luzLinterna.enabled)
            {
                luzLinterna.enabled = true;
            }
        }
    }
}
