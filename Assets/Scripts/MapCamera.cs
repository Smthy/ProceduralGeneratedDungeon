using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public Camera main, player;

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if(main.enabled == true)
            {
                player.enabled = true;
                main.enabled = false;
            }
            else if(player.enabled == true)
            {
                player.enabled = false;
                main.enabled = true;
            }
            else
            {
                player.enabled = true;
                main.enabled = false;
            }
        }
    }
}
