using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public int heal = 40;
    public GameObject ak, potion, key, player;
    public GameObject pistolInhand, akInHand, torch;
    private bool akUnlocked;

    public GameObject[] Doors;
    


    private void Start()
    {
        pistolInhand.SetActive(true);
        akInHand.SetActive(false);
        torch.SetActive(true);
        akUnlocked = false;

        Doors = GameObject.FindGameObjectsWithTag("FinalDoor");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
        {
            int layer_mask = LayerMask.GetMask("interact");
            
            RaycastHit hit;
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, layer_mask))
            {
                //Debug.Log(hit.transform.name);

                if (hit.collider.tag == "AK")
                {
                    Debug.Log("AK HIT");
                    akUnlocked = true;

                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.tag == "Potion")
                {
                    Debug.Log("Potion");

                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.IncreaseHealth(heal);
                    }

                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.tag == "Key")
                {
                    Debug.Log("Key Gained");
                    Destroy(hit.collider.gameObject);

                    foreach(GameObject door in Doors)
                    {
                        Destroy(door);
                    }
                }                
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(akUnlocked == true)
            {
                if(pistolInhand.activeSelf == true)
                {
                    akInHand.SetActive(true);
                    pistolInhand.SetActive(false);
                    torch.SetActive(false);
                }
                else
                {                    
                    pistolInhand.SetActive(true);
                    akInHand.SetActive(false);
                    torch.SetActive(true);
                }
                
            }
        }
    }
}
