using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            DesligarLasers desligarLasers = other.GetComponent<DesligarLasers>();

            if (desligarLasers != null)
            {
                desligarLasers.NotificarObserver();
            }
        }
    }
}
