using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public Dungeon startDungeonPre, endDungeonPre;
    public List<Dungeon> dungeonRooms = new List<Dungeon>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<Doorways> freeDoorways = new List<Doorways>();

    StarterRoom startDungeon;
    FinalRoom endDungeon;
    List<Dungeon> placedDungeons = new List<Dungeon>();

    LayerMask dunLayer;

    private void Awake()
    {
         dunLayer = LayerMask.GetMask("Dungeon");
    }

    void Start()
    {
        StartCoroutine("DungeonGeneration");
    }

    IEnumerator DungeonGeneration()
    {
        WaitForSeconds starting = new WaitForSeconds(0.5f);
        yield return starting;

        StartDungeonPlacement();
        yield return new WaitForFixedUpdate();

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i = 0; i < iterations; i++)
        {
            RoomPlacement();
            yield return new WaitForFixedUpdate();
        }
        
        EndDungeonPlacement();
        yield return new WaitForFixedUpdate();

        yield return new WaitForSeconds(1f);
        StopCoroutine("DungeonGeneration");

        ResetGeneration();
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

        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 dungeonDoorwayEuler = dungeonDoorway.transform.eulerAngles;
        
        float deltaAngle = Mathf.DeltaAngle(dungeonDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);

        dungeon.transform.rotation = currentRoomRotation * Quaternion.Euler(0, 180f, 0);

        Vector3 dungeonPositionOffset = dungeonDoorway.transform.position - dungeon.transform.position;
        dungeon.transform.position = targetDoorway.transform.position - dungeonPositionOffset;

    }

    bool CheckDungeonOverLap(Dungeon dungeon)
    {
        Collider[] hits = Physics.OverlapBox(dungeon.gameObject.transform.position, transform.localScale / 2, Quaternion.identity, dunLayer);
        if(hits.Length >= 0)
        {
            foreach(Collider c in hits)
            {
                if (c.transform.gameObject.Equals(dungeon.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    return true;
                }
            }
        }

        return false;       
    }   

    void EndDungeonPlacement()
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
        Debug.LogError("Reset level generator");

        StopCoroutine("DungeonGeneration");

        // Delete all rooms
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

        // Clear lists
        placedDungeons.Clear();
        freeDoorways.Clear();

        // Reset coroutine
        StartCoroutine("DungeonGeneration");

    }
}
