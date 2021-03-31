using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawning : MonoBehaviour
{
    public GameObject door1, door2, door3, door4;
    public Transform spawningPoint;

    public BoxCollider trigger;

    public GameObject enemy;

    private int maxAmount;
    int amount;


    private void Start()
    {
        maxAmount = Random.Range(1, 6);
        door1.SetActive(false);
        door2.SetActive(false);
        door3.SetActive(false);
        door4.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CloseDoors();
            SpawnAI();

            Destroy(trigger);

            StartCoroutine("OpenDoors");
        }
    }  

    void CloseDoors()
    {
        door1.SetActive(true);
        door2.SetActive(true);
        door3.SetActive(true);
        door4.SetActive(true);
    }   

    
    void SpawnAI()
    {
        for (int i = 0; i <= maxAmount; i++)
        {
            Instantiate(enemy, spawningPoint.position, Quaternion.Euler(0, 0, 0));
        }        
    }      

    IEnumerator OpenDoors()
    {
        yield return new WaitForSeconds(10f);

        Destroy(door1);
        Destroy(door2);
        Destroy(door3);
        Destroy(door4);
    }   
}
