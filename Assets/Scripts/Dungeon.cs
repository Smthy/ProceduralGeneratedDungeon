using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public Doorways[] doorways;
    public BoxCollider meshCol;

    public Bounds DungeonBounds
    {
        get { return meshCol.bounds; }
    }
}
