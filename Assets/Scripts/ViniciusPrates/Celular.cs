using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celular : MonoBehaviour
{
    public bool comecarAberto = false;
    void Start()
    {
        GameManager.instance.celularMenuUI = gameObject;
        gameObject.SetActive(comecarAberto);
    }
}
