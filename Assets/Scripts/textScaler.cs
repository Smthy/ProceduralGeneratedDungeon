using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textScaler : MonoBehaviour
{
    public float scale;

    public GameObject text;
    private GameObject newText;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        StartCoroutine("Scaler");
    }

    IEnumerator Scaler()
    {
        scale = Random.Range(1f, 1.75f);

        text.transform.localScale = new Vector3(scale, scale, scale);
        
        Vector3.SmoothDamp(text.transform.localScale, newText.transform.localScale, ref velocity, smoothTime);
        //newText.transform.localScale = text.transform.localScale;
        
        yield return new WaitForSeconds(1f);
        StartCoroutine("Scaler");

        //Vector3.SmoothDamp(text.transform.localScale, newText, ref velocity, smoothTime);
    }
}
