using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform player;
    public float mouseSen = 10f;

    private float x = 0;
    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x += -Input.GetAxis("Mouse Y") * mouseSen;
        y += Input.GetAxis("Mouse X") * mouseSen;

        x = Mathf.Clamp(x, -90, 90);

        transform.localRotation = Quaternion.Euler(x, 0, 0);
        player.transform.localRotation = Quaternion.Euler(0, y, 0);
    }
}
