using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration : MonoBehaviour
{
    public GameObject dungeonRoom;
    public int numberOfRooms = 10;
    public bool doorTop, doorBottom, doorLeft, doorRight;
    public Vector3 pos, spawn;
    public float dungeonX, dungeonZ, xMin, xMax, zMin, zMax;
    public Collider collisionZone;
    private float[] position;


    // Start is called before the first frame update
    void Start()
    {
        pos = dungeonRoom.GetComponent<Transform>().position;
        dungeonX = 0f;
        dungeonZ = 0f;
        DungeonRoomGen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DungeonRoomGen()
    {
        for(int roomIndex = 0; roomIndex < numberOfRooms; roomIndex++)
        {
            //pos = new Vector3();

            DungeonType(pos + new Vector3(dungeonX, 0, dungeonZ));
            dungeonX = Random.Range(xMin, xMax);
            dungeonZ = Random.Range(zMin, zMax);

            spawn = new Vector3(dungeonX, 0, dungeonZ);           
        }
    }  

    void DungeonType(Vector3 pos)
    {
        Instantiate(dungeonRoom, spawn, Quaternion.Euler(0, 0, 0));
    }
}
