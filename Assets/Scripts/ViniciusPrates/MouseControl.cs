using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] GameObject light;
    
    public void OnMouseEnter()
    {
        light.SetActive(true);
    }

    public void OnMouseExit()
    {
        light.SetActive(false);
    }
}
