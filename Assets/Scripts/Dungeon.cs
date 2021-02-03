using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public Doorways[] doorways;
    public BoxCollider boxCol;

    public Bounds DungeonBounds
    {
        get { return boxCol.bounds; }
    }
}
