using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celular : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.celularMenuUI = gameObject;
        gameObject.SetActive(false);
    }
}
