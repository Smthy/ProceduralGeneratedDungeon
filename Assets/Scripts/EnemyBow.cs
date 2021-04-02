using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBow : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public LayerMask ground, playerLayer;

    public GameObject arrow;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    public Vector3 destinationPoint;
    bool destinationSet;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(playerInSight && !playerInAttackRange)
        {
            following();
        }

        if(playerInSight && playerInAttackRange)
        {
            Attack();
        }
    }

    void following()
    {
        ai.SetDestination(player.position);
    }

    void Attack()
    {
        ai.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, 90f)).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
            rb.AddForce(transform.up * 10f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }



}
