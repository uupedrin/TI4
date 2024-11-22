using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Quadro : MonoBehaviour
{
    public GameObject panel;
    public GameObject objectToToggle;
    public Button closeButton; 
    public Collider colliderToToggle1; 
    public Collider colliderToToggle2; 

    private bool isPanelActive = false;

    void Start()
    {
        
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(DeactivatePanel);
        }

       
        if (colliderToToggle1 != null)
        {
            colliderToToggle1.enabled = true;
        }

        if (colliderToToggle2 != null)
        {
            colliderToToggle2.enabled = true;
        }
    }

    void OnMouseDown()
    {
        isPanelActive = !isPanelActive;

       
        panel.SetActive(isPanelActive);
        objectToToggle.SetActive(!isPanelActive);

        
        if (colliderToToggle1 != null)
        {
            colliderToToggle1.enabled = !isPanelActive;
        }

        if (colliderToToggle2 != null)
        {
            colliderToToggle2.enabled = !isPanelActive;
        }
    }

    // Método que desativa o painel
    public void DeactivatePanel()
    {
        panel.SetActive(false);
        objectToToggle.SetActive(true); // Ativa o objeto quando o painel é desativado
        isPanelActive = false;

        // Ativa os colisores quando o painel é desativado
        if (colliderToToggle1 != null)
        {
            colliderToToggle1.enabled = true;
        }

        if (colliderToToggle2 != null)
        {
            colliderToToggle2.enabled = true;
        }
    }
}