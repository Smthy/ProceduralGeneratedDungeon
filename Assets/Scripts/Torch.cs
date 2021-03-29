using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject torch;
    private bool torchOn;

    private void Start()
    {
        if(torch == null)
        {
            torch = GameObject.FindGameObjectWithTag("Torch");
        }

        torchOn = true;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.L))
        {
            if (torchOn == true)
            {
                torchOn = false;
                torch.SetActive(false);
            }
            else if (torchOn == false)
            {
                torchOn = true;
                torch.SetActive(true);
            }
            else
            {
                Debug.LogError("ERROR TORCH NOT FOUND");
            }
        }
    }
}
