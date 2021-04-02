using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWander : MonoBehaviour
{
    public NavMeshAgent ai;

    public Vector3 destination;
    bool destinationset;
    public float walkrange;

    public LayerMask ground;

    public float minX, maxX, minZ, maxZ;

    private void Awake()
    {
        ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        wander();
        
    }

    void wander()
    {
        if (!destinationset)
        {
            SearchDestination();
        }

        if(destinationset)
        {
            ai.SetDestination(destination);
        }

        Vector3 distanceToPoint = transform.position - destination; 

        if(distanceToPoint.magnitude < 1f)
        {
            destinationset = false;
        }
    }

    void SearchDestination()
    {
        float rx = Random.Range(minX, maxX);
        float rz = Random.Range(minZ, maxZ);

        destination = new Vector3(transform.position.x + rx, transform.position.y, transform.position.z + rz);

        if (Physics.Raycast(destination, -transform.up, 2f, ground))
        {
            destinationset = true;
        }
            
    }



}
