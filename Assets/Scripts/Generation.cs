using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Generation : MonoBehaviour
{      
    public int dungeonMin, dungeonMax;

    private Vector3 startPos = new Vector3(0f, 0f, 0f);
    
    public List<Dungeon> dungeonRooms = new List<Dungeon>();
    List<Dungeon> placedDungeons = new List<Dungeon>();
    List<Doorways> freeDoorways = new List<Doorways>();

    public Dungeon startDungeonPre, endDungeonPre;
    Dungeon recentDungeon, currentDungeon;
    StarterRoom startDungeon;
    FinalRoom endDungeon;

    public GameObject player;
    public Camera cam;
    public Canvas canvas;

    private Vector3 dungeonOffset, targetDoorwayAngle, dungeonDoorwayAngle;

    LayerMask dunLayer;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.SetActive(false);
        cam.enabled = true;
        canvas.enabled = false;
        dunLayer = LayerMask.GetMask("Dungeon");
        RenderSettings.fog = false;

        StartCoroutine("DungeonGeneration");
    }

    void Start()
    {
              
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
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

        StartCoroutine("NavMeshFinder");

        player.SetActive(true);
        cam.enabled = false;
        canvas.enabled = true;
        RenderSettings.fog = true;

        StopCoroutine("DungeonGeneration");
        //ResetGeneration(); //--- Used to show different maps being made
    }

    IEnumerator NavMeshFinder()
    {
        yield return new WaitForFixedUpdate();

        NavMeshSurface[] surfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
            
        }

        print("Built");
        StopCoroutine("NavMeshFinder");
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
            int doorwayNumber = Random.Range(0, list.Count);
            list.Insert(doorwayNumber, doorways);
        }
    }

    void RoomPlacement()
    {                       
        currentDungeon = Instantiate(dungeonRooms[Random.Range(0, dungeonRooms.Count)]);        

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
                recentDungeon = currentDungeon;
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
        dungeon.transform.position = startPos;
        dungeon.transform.rotation = Quaternion.identity;

        targetDoorwayAngle = targetDoorway.transform.eulerAngles;
        dungeonDoorwayAngle = dungeonDoorway.transform.eulerAngles;
        
        float deltaAngle = Mathf.DeltaAngle(dungeonDoorwayAngle.y, targetDoorwayAngle.y);
        Quaternion currentRoomRotation = Quaternion.AngleAxis(deltaAngle, -Vector3.down);
        dungeon.transform.rotation = currentRoomRotation * Quaternion.Euler(0f, 180f, 0f);

        dungeonOffset = dungeonDoorway.transform.position - dungeon.transform.position;
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
                    Debug.Log("No COllision");
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
            
            if(freeDoorways.Count == 4)
            {
                ResetGeneration();
            }

            break;


        }

        if(!dungeonPlaced)
        {
            ResetGeneration();
        }        
    }

    void ResetGeneration()
    {
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
        canvas.enabled = false;
        cam.enabled = true;

        StartCoroutine("DungeonGeneration");

    }
}
