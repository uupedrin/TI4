using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFixColliders : MonoBehaviour
{
    public GameObject panel; 
    public Collider collider1; 
    public Collider collider2; 
    public void OnButtonPressed()
    {
        if (panel != null)
        {
            panel.SetActive(false); 
        }

        if (collider1 != null)
        {
            collider1.enabled = true;
        }

        if (collider2 != null)
        {
            collider2.enabled = true; 
        }
    }
}
