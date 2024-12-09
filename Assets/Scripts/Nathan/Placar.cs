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
            scoreText.text = GameManager.instance.score.ToString();
            healthText.text = CrossSceneReference.instance.playerController.health.ToString();
        }
    }
}
