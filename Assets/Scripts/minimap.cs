using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}
