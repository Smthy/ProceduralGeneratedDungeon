using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawning : MonoBehaviour
{
    public GameObject door1, door2, door3, door4;
    public GameObject[] corners;
    private GameObject currentCorner;
    private int index;

    public BoxCollider trigger;

    public GameObject[] enemies;
    private GameObject enemy;
    private int enemyIndex;

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
            index = Random.Range(0, corners.Length);
            currentCorner = corners[index];
            Vector3 spawningPoint = new Vector3(currentCorner.transform.position.x, currentCorner.transform.position.y, currentCorner.transform.position.z);

            enemyIndex = Random.Range(0, enemies.Length);
            enemy = enemies[enemyIndex];
            Instantiate(enemy, spawningPoint, Quaternion.Euler(0, 0, 0));
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
