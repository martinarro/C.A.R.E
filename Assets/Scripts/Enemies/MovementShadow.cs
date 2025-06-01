using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShadow : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    public float coolDown;

    // Update is called once per frame
    void Update()
    {
        coolDown += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (coolDown > 10)
        {
            Destroy(gameObject);
        }
    }
}
