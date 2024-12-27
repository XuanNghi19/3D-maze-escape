using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
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

    // basic index
    public int maxHp = 4;
    public int hp = 4;
    public int atk = 1;
    public int def = 0;

    // attack animation
    public bool isAttack = false;
    int attackIndex = 0;
    public float attackCooldown = 16/30f;
    float lastAttackTime = 0f;

    // sword collider
    public BoxCollider swordCollider;

    //Audio
    public AudioSource audioSrc;
    public AudioClip swordSnap, attackedSrc;

    //Attacked
    private bool canTakeDamage = true;
    private float damageCooldown = 1f;
    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        swordCollider = GetComponentInChildren<BoxCollider>();
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

        //< animation attack
        if (Input.GetMouseButtonDown(0) && !isAttack && Time.time > lastAttackTime + attackCooldown && !isDead)
        {
            isAttack = true;
            lastAttackTime = Time.time;
            attackIndex = (attackIndex + 1) % 4;
            animator.SetTrigger("attack" + attackIndex);

            audioSrc.PlayOneShot(swordSnap);
        }
        //>

        //< animation movement
        float x = 0;
        float z = 0;
        if (!isAttack && !isDead)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) x = Input.GetAxis("Horizontal");
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) z = Input.GetAxis("Vertical");
        }

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

        //< gravity
        velocity.y += gravity * Time.deltaTime;
        //>

        //< move player
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
        //>
        
        //< Reset attack state after cooldown
        if (isAttack && Time.time > lastAttackTime + attackCooldown)
        {
            isAttack = false;
        }
        //>
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && canTakeDamage && !isDead)   
        {
            canTakeDamage = false;
            audioSrc.PlayOneShot(attackedSrc);
            StartCoroutine(DamageCooldown(
                GetDamage(other)
            ));
            Debug.Log("Collision with monster detected by playerCollider!");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster") && canTakeDamage && !isDead)
        {
            canTakeDamage = false;
            audioSrc.PlayOneShot(attackedSrc);
            StartCoroutine(DamageCooldown(GetDamage(other)));
            Debug.Log("Collision with monster detected by playerCollider!");
        }
    }

    private IEnumerator DamageCooldown(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Debug.Log("Player is dead!");
            animator.SetTrigger("isDead");
            isDead = true;
        }
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }

    private int GetDamage(Collider other)
    {
        int monsterAtk = other.GetComponent<Monster>().atk;
        return monsterAtk - def;
    }
}