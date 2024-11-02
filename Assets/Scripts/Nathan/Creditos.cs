using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public GameObject panel;
    public Collider colliderToToggle;  

    private bool isPanelActive = false;

    void OnMouseDown()
    {
        
        isPanelActive = !isPanelActive;

        
        panel.SetActive(isPanelActive);

        
        if (colliderToToggle != null)
        {
            colliderToToggle.enabled = !isPanelActive;
        }
    }
}