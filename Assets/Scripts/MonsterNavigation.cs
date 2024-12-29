using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MonsterNavigation : MonoBehaviour
{

    private Player player;
    private NavMeshAgent agent;
    public Vector3 startPos;
    public float speed = 3.5f; // Thêm thuộc tính tốc độ
    public float distantRanege = 10f;
    // khoang cach giua monster va player
    public float stopDistance = 1.5f;

    // animation
    public Animator anim;
    public float attackRange = 2.0f;
    private bool isMovement = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        // Đặt tốc độ cho NavMeshAgent
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(Attack());
            }
        }

        if (player.isDead || distanceToPlayer > distantRanege)
        {
            agent.destination = startPos;
        } else{
            MoveTowardsPlayer(distanceToPlayer);
        }
        CheckMovement();
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistance)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Vector3 targetPosition = player.transform.position - direction * stopDistance;
            agent.destination = targetPosition;
        }
        else
        {
            agent.destination = transform.position;
        }

        // Rotate to face the player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        lookDirection.y = 0; // Keep only the horizontal direction
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    // Check if the monster is moving
    void CheckMovement()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            isMovement = true;
        }
        else
        {
            isMovement = false;
        }

        anim.SetBool("isMovement", isMovement);
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        isAttacking = true;
        yield return new WaitForSeconds(5/3f);
        isAttacking = false;
    }

}
