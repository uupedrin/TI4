using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEATLOGO : MonoBehaviour
{
    [SerializeField] private GameObject logo;

    // Update is called once per frame
    void Update()
    {
        logo.SetActive(GameManager.instance.cheat);
    }
}
