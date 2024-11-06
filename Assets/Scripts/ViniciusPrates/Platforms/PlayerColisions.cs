using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColisions : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // if(col.transform.CompareTag(""))
        // {

        // }
        // if(col.transform.CompareTag(""))
        // {
            
        // }
        if(hit.transform.CompareTag("Obstaculo"))
        {
            //GetDamage do player
        }
        if(hit.transform.CompareTag("Espinhos"))
        {
            Debug.Log("Morte");
        }
        if(hit.transform.CompareTag("Mola"))
        {
            if(hit.transform.position.y < transform.position.y)
            player.Mola();
        }

        PlatformTremble platform;
        if(hit.transform.TryGetComponent<PlatformTremble>(out platform))
            platform.ActiveRoutine();
    }
}
