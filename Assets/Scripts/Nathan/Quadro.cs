using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Necessário para trabalhar com UI

public class Quadro : MonoBehaviour
{
    public GameObject panel;  
    public GameObject objectToToggle;
    public Button closeButton;  // Botão para desativar o painel

    private bool isPanelActive = false; 

    void Start()
    {
        // Verifica se o botão foi atribuído e adiciona o listener para o método de desativação do painel
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(DeactivatePanel);
        }
    }

    void OnMouseDown()
    {
        isPanelActive = !isPanelActive;

        // Ativa ou desativa o painel e o objeto alternadamente
        panel.SetActive(isPanelActive);
        objectToToggle.SetActive(!isPanelActive);
    }

    // Método que desativa o painel
    public void DeactivatePanel()
    {
        panel.SetActive(false);
        objectToToggle.SetActive(true);  // Ativa o objeto quando o painel é desativado
        isPanelActive = false;
    }
}