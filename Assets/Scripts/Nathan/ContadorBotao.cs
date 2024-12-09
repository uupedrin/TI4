using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContadorBotao : MonoBehaviour
{
    public int maxClicks = 6; // Número máximo de cliques
public int clickCount = 0; // Contador de cliques
public Button[] buttons; // Botões para gerenciar
public Button resetButton; // Botão para resetar
public Sprite newSprite; // Sprite para alterar botões
public TextMeshProUGUI clickCountText; // Referência para o TextMeshPro na UI

private List<Button> clickedButtons = new List<Button>(); // Lista de botões clicados
public Drone drone; // Referência ao script do Drone

void Start()
{
    foreach (Button button in buttons)
    {
        button.onClick.AddListener(() => OnButtonClicked(button));
    }

    if (resetButton != null)
    {
        resetButton.onClick.AddListener(OnResetButtonClicked);
    }

    UpdateClickCountText();
}

void OnButtonClicked(Button button)
{
    clickCount++;

    if (!clickedButtons.Contains(button))
    {
        clickedButtons.Add(button);
    }

    UpdateClickCountText();

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
    // Verifica se o drone está na posição inicial
    if (drone != null && Vector3.Distance(drone.transform.position, drone.startT) > 0.01f)
    {
        Debug.Log("O drone não está na posição inicial. Não é possível remover a última mesa.");
        return;
    }

    if (clickCount > 0)
    {
        clickCount--;

        // Remover a última mesa (e não a penúltima)
        if (drone != null)
        {
            //drone.DestruirUltimaMesa(); // Remover a última mesa colocada
        }

        // Alterar o sprite do botão após a remoção da mesa
        if (clickedButtons.Count > 0)
        {
            // Alterar o sprite do último botão clicado
            Button lastClickedButton = clickedButtons[clickedButtons.Count - 1];
            Image buttonImage = lastClickedButton.GetComponent<Image>();
            if (buttonImage != null && newSprite != null)
            {
                buttonImage.sprite = newSprite;
            }

            clickedButtons.RemoveAt(clickedButtons.Count - 1);
        }

        if (clickCount < maxClicks)
        {
            foreach (Button btn in buttons)
            {
                btn.interactable = true;
            }
        }

        UpdateClickCountText();
    }
}

void UpdateClickCountText()
{
    if (clickCountText != null)
    {
        clickCountText.text = $"Mesas: {clickCount}/{maxClicks}";
    }
}
}