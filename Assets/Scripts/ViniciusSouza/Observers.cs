using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observers : MonoBehaviour, IObserver
{
    public void Notificar()
    {
        gameObject.SetActive(false);
    }
}
