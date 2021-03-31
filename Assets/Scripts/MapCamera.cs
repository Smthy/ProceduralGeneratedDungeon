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
                RenderSettings.fog = true;
                player.enabled = true;
                main.enabled = false;
            }
            else if(player.enabled == true)
            {
                //RenderSettings.fog = false;
                player.enabled = false;
                main.enabled = true;
            }
            else
            {
                RenderSettings.fog = true;
                player.enabled = true;
                main.enabled = false;
            }
        }
    }
}
