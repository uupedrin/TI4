using UnityEngine;

public class QuadroBotao : MonoBehaviour
{
    public GameObject panelToDisable;  // Painel que será desativado
    public GameObject objectToEnable;  // Objeto que será ativado

    void Start()
    {
        // Garante que o objeto esteja desativado no início
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false);
        }
    }

    // Método para ser chamado no botão
    public void Toggle()
    {
        // Verifica se o painel e o objeto estão configurados corretamente
        if (panelToDisable != null && objectToEnable != null)
        {
            // Desativa o painel e ativa o objeto
            panelToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }
    }
}