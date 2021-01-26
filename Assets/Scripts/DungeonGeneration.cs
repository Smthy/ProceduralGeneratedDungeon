using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public GameObject dungeonRoom, corridorNS, corridorEW;
    public int numberOfRooms;
    public bool doorTop, doorBottom, doorLeft, doorRight;
    public Vector3 pos;
    public float dungeonX, dungeonZ;

    private void Start()
    {
        numberOfRooms = 7;
        pos = dungeonRoom.GetComponent<Transform>().position;
        dungeonX = 0f;
        dungeonZ = 0f;
        DungeonRoomGen();
    }

    private void DungeonRoomGen()
    {
        for(int roomIndex = 0; roomIndex <numberOfRooms; roomIndex++)
        {
            pos = new Vector3();
            DungeonType(pos + new Vector3(dungeonX, 0, dungeonZ));
            


            DungeonType(pos);
        }      
    }

    private void DungeonType(Vector3 pos)
    {
        Instantiate(dungeonRoom);
    }
}
