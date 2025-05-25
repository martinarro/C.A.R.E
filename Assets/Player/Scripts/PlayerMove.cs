using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded = true;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("escalera"))
        {
            Debug.Log("En contacto con la escalera");
            speed = 15;
            rb.useGravity = false;

                if (Input.GetKey(KeyCode.W))
            {
                Vector3 climbMovement = Vector3.up * speed * Time.deltaTime;
                rb.MovePosition(rb.position + climbMovement);
            }
        }
        
        
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("escalera"))
        {
            rb.useGravity = true;
            speed = 5;
        }
    }
    

}
