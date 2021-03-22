using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeLights : MonoBehaviour
{
    public Light lights;
    public int[] biomes;
    private int currentBiome; 
    private int index;
    void Start()
    {
        //Custom Light colors, instead of the default colours. RGBA/255 to work out the color of the lights.
        //Each light represents biomes in the world. Enemies will corrospond.

        lights.GetComponent<Light>();
        index = Random.Range(0, biomes.Length);
        currentBiome = biomes[index];

        if(currentBiome == 1)
        {

           lights.color = new Color(0.113f, 0.427f, 0.196f, 1f);
           
        }
        else if (currentBiome == 2)
        {
            lights.color = new Color(0.561f, 0.039f, 0.039f, 1f);
            
        }
        else if (currentBiome == 3)
        {
            lights.color = new Color(0.125f, 0.372f, 0.396f, 1f);
            
        }
        else if (currentBiome == 4)
        {
            lights.color = new Color(0.529f, 0.467f, 0.130f, 1f);
            
        }
        else
        {
            lights.color = new Color(0.380f, 0.384f, 0.376f, 1f);
            
        }
    }

    
}
