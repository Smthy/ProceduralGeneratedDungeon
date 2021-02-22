using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public Doorways[] doorways;
    public MeshCollider meshCol;

    public Bounds DungeonBounds
    {
        get { return meshCol.bounds; }
    }
}
