using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzVerde : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private Color cor;
    [SerializeField] private bool t1,t2;
    
    void Update()
    {
        if(t1 && GameManager.instance.vitoria1)
        light.color = cor;
        if(t2 && GameManager.instance.vitoria2)
        light.color = cor;
    }
}
