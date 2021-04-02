using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipChanger : MonoBehaviour
{
    public Text tipText;

    public int[] randomTips;
    private int index;
    private int currentTip;

    private void Start()
    {
        StartCoroutine("Changer");
    }

    IEnumerator Changer()
    {
        index = Random.Range(0, randomTips.Length);

        currentTip = randomTips[index];

        if (currentTip == 1)
        {
            tipText.text = ("Tip: Play with Sound :)");
        }
        else if (currentTip == 2)
        {
            tipText.text = ("Tip: The AK is better");
        }
        else if (currentTip == 3)
        {
            tipText.text = ("Tip: Remember where the white cube is");
        }
        else if (currentTip == 4)
        {
            tipText.text = ("Tip: *Insert Bee Movie Script*");
        }
        else if (currentTip == 6)
        {
            tipText.text = ("Tip: I believe in you!");
        }
        else if (currentTip == 7)
        {
            tipText.text = ("Tip: Don't give up");
        }
        else if (currentTip == 8)
        {
            tipText.text = ("Tip: Make Sure you keep your eyes open");
        }
        else if (currentTip == 9)
        {
            tipText.text = ("Tip: Stay away from the arrows");
        }
        else if (currentTip == 10)
        {
            tipText.text = ("Tip: Don't waste the potions");
        }
        else if (currentTip == 11)
        {
            tipText.text = ("Tip: Not Found");
        }    

        yield return new WaitForSeconds(5f);
        StartCoroutine("Changer");
    }


}
