using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private TextMeshProUGUI healthText; 

    void Update()
    {
        
        if (GameManager.instance != null)
        {
            scoreText.text = "Pontos: " + GameManager.instance.score;
            healthText.text = "Vida: " + CrossSceneReference.instance.playerController.health;
        }
    }
}
