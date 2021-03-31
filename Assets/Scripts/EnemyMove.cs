using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMesAgent;
    public GameObject player;

    public bool travelling;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMesAgent = GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        Vector3 targetVector;

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
}
