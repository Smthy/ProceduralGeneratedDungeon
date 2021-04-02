using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpin : MonoBehaviour
{
    float xSpin;
    public float speed;

    void Start()
    {
        xSpin = Random.Range(0, 360);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, xSpin, 0) * speed * Time.deltaTime);
    }
}
