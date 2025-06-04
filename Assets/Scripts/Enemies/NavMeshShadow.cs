using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshShadow : MonoBehaviour
{
    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;
    [SerializeField] public NavMeshAgent agent;

    [SerializeField] public float health = 100;
    public Transform actualPoint;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        actualPoint = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (actualPoint == pointA)
            {
                actualPoint = pointB;
            }
            else
            {
                actualPoint = pointA;
            }

            agent.SetDestination(actualPoint.position);

        }


    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
