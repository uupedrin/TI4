using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastBotao : MonoBehaviour
{
    
    /*
    public float raycastRange = 5f;
    public LayerMask butaoLayerMask;
    public Transform origem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if(Physics.Raycast(origem.position, origem.forward, out hit, raycastRange, butaoLayerMask)) 
            { 
                DesligarLasers laser = hit.collider.GetComponent<DesligarLasers>();
                if(laser != null)
                {
                    laser.NotificarObserver();
                }
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origem.position, origem.forward * raycastRange);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Botao"))
        { 
            DesligarLasers desligarlaser = other.GetComponent<DesligarLasers>();

            if (desligarlaser != null)
                desligarlaser.NotificarObserver();
        }
    }
}
