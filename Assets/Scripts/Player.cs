using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public float wallDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask wallMask;
    Vector3 velocity;
    bool isGrounded;
    bool isWall;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //< check hit ground and wall
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isWall = Physics.CheckSphere(transform.position, wallDistance, wallMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //>

        //< animation movement
        float x = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) x = Input.GetAxis("Horizontal");
        float z = 0;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            animator.SetBool("isMovement", true);
            animator.SetFloat("vertical", z);
            animator.SetFloat("horizontal", x);

        }
        else
        {
            animator.SetBool("isMovement", false);
            animator.SetFloat("vertical", 0);
            animator.SetFloat("horizontal", 0);
        }

        //>

        //< movement
        Vector3 move = transform.right * x + transform.forward * z;
        if (isWall)
        {
            move = Vector3.zero;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //>

        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}