using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{      
    public int dungeonMin, dungeonMax;
    public Camera cam;
    
    public List<Dungeon> dungeonRooms = new List<Dungeon>();
    List<Dungeon> placedDungeons = new List<Dungeon>();
    List<Doorways> freeDoorways = new List<Doorways>();

    public Dungeon startDungeonPre, endDungeonPre;
    StarterRoom startDungeon;
    FinalRoom endDungeon;

    public GameObject player;

    LayerMask dunLayer;

    private void Awake()
    {
        player.SetActive(false);
        cam.enabled = true;

        dunLayer = LayerMask.GetMask("Dungeon");
    }

    void Start()
    {       
        StartCoroutine("DungeonGeneration");
    }

    IEnumerator DungeonGeneration()
    {
        yield return new WaitForSeconds(0.1f);
        StartDungeonPlacement();       
        int iterations = Random.Range(dungeonMin, dungeonMax);

        for(int x = 0; x < iterations; x++)
        {
            RoomPlacement();
            yield return new WaitForFixedUpdate(); //allows the physics to update, otherwise the rooms aren't generated properly
        }
        FinalDungeonPlacement();
        yield return new WaitForSeconds(1f);

        player.SetActive(true);
        cam.enabled = false;

        StopCoroutine("DungeonGeneration");
        //ResetGeneration(); //--- Used to show different maps being made
    }

    void StartDungeonPlacement()
    {
        startDungeon = Instantiate(startDungeonPre) as StarterRoom;
        startDungeon.transform.parent = this.transform;
        DoorWayAdd(startDungeon, ref freeDoorways);

        startDungeon.transform.position = Vector3.zero;
        startDungeon.transform.rotation = Quaternion.identity;
    }
    void DoorWayAdd(Dungeon dungeon, ref List<Doorways> list)
    {
        foreach(Doorways doorways in dungeon.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorways);
        }
    }

    //
    void RoomPlacement()
    {
        Dungeon currentDungeon = Instantiate(dungeonRooms[Random.Range(0, dungeonRooms.Count)]) as Dungeon;
        currentDungeon.transform.parent = this.transform;

        List<Doorways> AllfreeDoorways = new List<Doorways>(freeDoorways);
        List<Doorways> currentDungeonDoorWays = new List<Doorways>();

        DoorWayAdd(currentDungeon, ref currentDungeonDoorWays);
        DoorWayAdd(currentDungeon, ref freeDoorways);

        bool dungeonPlaced = false;

        foreach(Doorways freeDoorway in AllfreeDoorways)
        {
            foreach(Doorways currentDoorways in currentDungeonDoorWays)
            {
                PositionDungeonAtDoor(ref currentDungeon, currentDoorways, freeDoorway);

                if (CheckDungeonOverLap(currentDungeon))
                {
                    continue;                    
                }

                dungeonPlaced = true;
                placedDungeons.Add(currentDungeon);
                currentDoorways.gameObject.SetActive(false);
                freeDoorways.Remove(currentDoorways);
                freeDoorway.gameObject.SetActive(false);
                freeDoorways.Remove(freeDoorway);

                break;               
            }

            if (dungeonPlaced)
            {
                break;
            }
        }
        
        if(!dungeonPlaced)
        {
            Destroy(currentDungeon.gameObject);
            ResetGeneration();
        }
    }

    void PositionDungeonAtDoor(ref Dungeon dungeon, Doorways dungeonDoorway, Doorways targetDoorway)
    {
        dungeon.transform.position = Vector3.zero;
        dungeon.transform.rotation = Quaternion.identity;

        Vector3 targetDoorwayAngle = targetDoorway.transform.eulerAngles;
        Vector3 dungeonDoorwayAngle = dungeonDoorway.transform.eulerAngles;
        
        float angle = Mathf.DeltaAngle(dungeonDoorwayAngle.y, targetDoorwayAngle.y);
        Quaternion currentRoomRotation = Quaternion.AngleAxis(angle, Vector3.up);

        dungeon.transform.rotation = currentRoomRotation * Quaternion.Euler(0, 180f, 0);

        Vector3 dungeonOffset = dungeonDoorway.transform.position - dungeon.transform.position;
        dungeon.transform.position = targetDoorway.transform.position - dungeonOffset;

    }

    bool CheckDungeonOverLap(Dungeon dungeon)
    {
        Collider[] hits = Physics.OverlapBox(dungeon.gameObject.transform.position, transform.localScale / 2, Quaternion.identity, dunLayer);
        if(hits.Length >= 0)
        {
            foreach(Collider floorCollision in hits)
            {
                if (floorCollision.transform.gameObject.Equals(dungeon.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.Log("Collision " +transform.position);
                    return true;
                }
            }
        }

        return false;       
    }   

    void FinalDungeonPlacement()
    {
        endDungeon = Instantiate(endDungeonPre) as FinalRoom;
        endDungeon.transform.parent = this.transform;


        List<Doorways> AllfreeDoorways = new List<Doorways>(freeDoorways);
        Doorways doorways = endDungeon.doorways[0];

        bool dungeonPlaced = false;

        foreach (Doorways freeDoorway in AllfreeDoorways)
        {
            Dungeon dungeon = (Dungeon)endDungeon;
            PositionDungeonAtDoor(ref dungeon, doorways, freeDoorway);

            if (CheckDungeonOverLap(endDungeon))
            {
                continue;
            }

            dungeonPlaced = true;

            doorways.gameObject.SetActive(false);
            freeDoorways.Remove(doorways);

            freeDoorway.gameObject.SetActive(false);
            freeDoorways.Remove(freeDoorway);

            break;
        }

        if(!dungeonPlaced)
        {
            ResetGeneration();
        }        
    }

    void ResetGeneration()
    {
        Debug.LogError("Level Generation Restarted");
        StopCoroutine("DungeonGeneration");

        
        if (startDungeon)
        {
            Destroy(startDungeon.gameObject);
        }

        if (endDungeon)
        {
            Destroy(endDungeon.gameObject);
        }

        foreach (Dungeon dungeon in placedDungeons)
        {
            Destroy(dungeon.gameObject);
        }
                
        placedDungeons.Clear();
        freeDoorways.Clear();

        player.SetActive(false);
        cam.enabled = true;


        StartCoroutine("DungeonGeneration");

    }
}
