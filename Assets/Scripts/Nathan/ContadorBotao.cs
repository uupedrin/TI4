using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o namespace do TextMeshPro


public class ContadorBotao : MonoBehaviour
{
    public int maxClicks = 6; // Número máximo de cliques
    public int clickCount = 0; // Contador de cliques
    public Button[] buttons; // Botões para gerenciar
    public Button resetButton; // Botão para resetar
    public Sprite newSprite; // Sprite para alterar botões
    public TextMeshProUGUI clickCountText; // Referência para o TextMeshPro na UI

    private List<Button> clickedButtons = new List<Button>(); // Lista de botões clicados

    void Start()
    {
        // Configura listeners para os botões
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }

        // Listener para o botão de resetar
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetButtonClicked);
        }

        // Atualiza o texto inicial no TextMeshPro
        UpdateClickCountText();
    }

    void OnButtonClicked(Button button)
    {
        clickCount++; // Incrementa o contador

        // Adiciona o botão à lista se ainda não estiver nela
        if (!clickedButtons.Contains(button))
        {
            clickedButtons.Add(button);
        }

        // Atualiza o texto no TextMeshPro
        UpdateClickCountText();

        // Desativa os botões se o número máximo de cliques for atingido
        if (clickCount >= maxClicks)
        {
            foreach (Button btn in buttons)
            {
                btn.interactable = false;
            }
        }
    }

    void OnResetButtonClicked()
    {
        if (clickCount > 0)
        {
            clickCount--; // Decrementa o contador

            // Altera o sprite do primeiro botão clicado e remove-o da lista
            if (clickedButtons.Count > 0)
            {
                Button firstClicked = clickedButtons[0];
                Image buttonImage = firstClicked.GetComponent<Image>();
                if (buttonImage != null && newSprite != null)
                {
                    buttonImage.sprite = newSprite;
                }

                clickedButtons.RemoveAt(0);
            }

            // Reativa os botões se os cliques forem menores que o máximo
            if (clickCount < maxClicks)
            {
                foreach (Button btn in buttons)
                {
                    btn.interactable = true;
                }
            }

            // Atualiza o texto no TextMeshPro
            UpdateClickCountText();
        }
    }

    // Método para atualizar o texto do contador no TextMeshPro
    void UpdateClickCountText()
    {
        if (clickCountText != null)
        {
            clickCountText.text = $"Mesas: {clickCount}/{maxClicks}";
        }
    }
}