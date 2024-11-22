using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public GameObject panel;
    public Collider colliderToToggle1; 
    public Collider colliderToToggle2; 

    private bool isPanelActive = false;

    void OnMouseDown()
    {
        
        isPanelActive = !isPanelActive;
        panel.SetActive(isPanelActive);

        
        if (colliderToToggle1 != null)
        {
            colliderToToggle1.enabled = !isPanelActive;
        }

        if (colliderToToggle2 != null)
        {
            colliderToToggle2.enabled = !isPanelActive;
        }
    }
}