using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public int[] audios;
    private int index;
    private int currentAudio;
    public float minWait, maxWait;

    private void Start()
    {
        StartCoroutine("SwitchSong");
    }

    IEnumerator SwitchSong()
    {
        float waitTime = Random.Range(minWait, maxWait);

        yield return new WaitForSeconds(waitTime);

        index = Random.Range(0, audios.Length);

        currentAudio = audios[index];

        if(currentAudio == 1)
        {
            FindObjectOfType<AudioManager>().Play("Wind");
        }

        if (currentAudio == 2)
        {
            FindObjectOfType<AudioManager>().Play("Chain");
        }

        if (currentAudio == 3)
        {
            FindObjectOfType<AudioManager>().Play("Water");
        }

        if (currentAudio == 4)
        {
            FindObjectOfType<AudioManager>().Play("Fire");
        }

    }
}
