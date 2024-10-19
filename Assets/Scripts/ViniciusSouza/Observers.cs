using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observers : MonoBehaviour, IObserver
{
    private DesligarLasers desligarLasers;

    void Start()
    {
        desligarLasers = FindObjectOfType<DesligarLasers>();

        if (desligarLasers != null)
        {
            desligarLasers.RegistrarObserver(this);
        }
    }

    public void Notificar()
    {
        Destroy(GetComponent<LineRenderer>());
        Destroy(GetComponent<Laser>());
    }
}
