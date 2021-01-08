using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public GameObject dungeonRoom, corridorNS, corridorEW;
    private int numberOfRooms;

    private void Start()
    {
        Instantiate(dungeonRoom);
    }
}
