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
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return starting;

        StartDungeonPlacement();
        yield return interval;

        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i = 0; i < iterations; i++)
        {
            RoomPlacement();
            yield return interval; 
        }
        //EndDungeonPlacement();
        yield return interval;

        yield return new WaitForSeconds(5);
        StopCoroutine("DungeonGeneration");
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

                if(CheckDungeonOverLap(currentDungeon))
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

            if(dungeonPlaced)
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
        Bounds bounds = dungeon.DungeonBounds;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, dungeon.transform.rotation, dunLayer);
        if(colliders.Length > 0)
        {
            foreach(Collider col in colliders)
            {
                if (col.transform.parent.gameObject.Equals(dungeon.gameObject))
                {
                    continue;
                }
                else
                {
                    print("COLLISION");
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

        bool roomPlaced = false;

    }

    void ResetGeneration()
    {
        StopCoroutine("DungeonGeneration");
        StopCoroutine("DungeonGeneration");
    }
   



}
