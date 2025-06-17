using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController player;

    private Animator anim;

    public float horizontalMove;
    public float verticalMove;
    public float playerSpeed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    private Vector3 movePlayer;
    private Vector3 playerInput;

    public Camera mainCamera;

    private Vector3 camForward;
    private Vector3 camRight;

    void Start()
    {
        anim = GetComponent<Animator>();

        //Bloquear cursor durante el Play
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        anim.SetFloat("VelX", horizontalMove);
        anim.SetFloat("VelY", verticalMove);

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
    }

    //Funcion para la direccion de la camara.
    void camDirection()
    { 
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Funcion para las habilidades del Player (Salto descartado por ahora).
    public void PlayerSkills()
    {
        /*if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }*/
    }


    //Funcion para la gravedad.
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}
