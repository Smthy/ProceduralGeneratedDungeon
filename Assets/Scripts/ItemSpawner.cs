using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    private GameObject currentItem;
    public Transform spawn;

    private int index;

    private void Start()
    {
        index = Random.Range(0, items.Length);
        currentItem = items[index];

        Instantiate(currentItem, spawn.position, Quaternion.identity);
    }
}
