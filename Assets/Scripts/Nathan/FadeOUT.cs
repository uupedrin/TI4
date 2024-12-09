using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOUT : MonoBehaviour
{
    public Image blackPanel;

    public Image BlackPanel2;
    public float fadeSpeed = 1f;

    public float tempoescuro;

    void Start()
    {
        if (blackPanel != null)
        {
            Invoke( nameof(Desativar),tempoescuro);
        }
    }

    IEnumerator FadeOut()
    {
        Color panelColor = blackPanel.color;
        while (panelColor.a > 0)
        {
            
            panelColor.a -= Time.deltaTime * fadeSpeed;
            blackPanel.color = panelColor;

            yield return null; 
        }

        
        panelColor.a = 0;
        blackPanel.gameObject.SetActive(false);
        blackPanel.color = panelColor;

        
        
    }

    void Desativar()
    {
       BlackPanel2.gameObject.SetActive(false);
       StartCoroutine(FadeOut());

    }
}