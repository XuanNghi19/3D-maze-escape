using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MonsterNavigation : MonoBehaviour
{

    public Player player;
    private NavMeshAgent agent;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            agent.destination = startPos;
        } else {
            agent.destination = player.transform.position;
        }
    }
}
