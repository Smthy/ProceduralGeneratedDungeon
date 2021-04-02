using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMesAgent;
    public GameObject player;

    public bool travelling;

    Vector3 targetVector;

    public int damage = 15;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesAgent = GetComponent<NavMeshAgent>();

    }

    void FixedUpdate()
    {       

        if (player.activeSelf == true)
        {
            targetVector = player.transform.position;
            navMesAgent.SetDestination(targetVector);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
