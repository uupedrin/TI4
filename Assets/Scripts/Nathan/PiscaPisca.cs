using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscaPisca : MonoBehaviour
{
    public Light lightSource; 
    public float intensityMin = 0f; 
    public float intensityMax = 300f; 
    public float interval = 1f; 
    private float timer;

    void Start()
    {
        if (lightSource == null)
            lightSource = GetComponent<Light>(); 

        timer = interval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        
        if (timer <= 0f)
        {
            
            lightSource.intensity = (lightSource.intensity == intensityMax) ? intensityMin : intensityMax;
            timer = interval; 
        }
    }
}