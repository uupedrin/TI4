using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int type; 
    [SerializeField] int scoreAdd; 
    public PlayerController player; //botei teu case inteiro no trigger paulao, nao gostou vem x1 praça liberdade

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case 0:
                    
                    GameManager.instance.SumScore(scoreAdd);
                    break;

                case 1:
                    
                    player.PuloDoploAbl = true;
                    break;

                case 2:
                    
                    player.correAbl = true;
                    break;
            }

            
            gameObject.SetActive(false);
        }
    }
}
