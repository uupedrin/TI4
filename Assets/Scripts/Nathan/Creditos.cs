using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    public GameObject panel;

    private bool isPanelActive = false; 

    void OnMouseDown()
    {
        
        isPanelActive = !isPanelActive;

        
        panel.SetActive(isPanelActive);
    }
}
