using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int type;
    [SerializeField] int scoreAdd;
    public PlayerController player;
    public GameManager instance;

    public void Collect()
    {
        switch(type)
        {
            case 0:
            instance.SumScore(scoreAdd);
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
