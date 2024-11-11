using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisorColisao : MonoBehaviour
{
    [SerializeField]ComportamentoInimigo comportamento;
    public void OnTriggerEnter(Collider col)
    {
        if(col.name == "Player")
        {
            // comportamento.SetDetectar(true);
            // comportamento.ChecandoArredor();
        }
    }
}
