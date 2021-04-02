using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrpwDeletion : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine("Deletion");
    }

    IEnumerator Deletion()
    {
        yield return new WaitForSeconds(2f);

        Destroy(this.gameObject);

    }
}
