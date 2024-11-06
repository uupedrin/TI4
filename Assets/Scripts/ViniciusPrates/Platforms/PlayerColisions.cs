using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColisions : MonoBehaviour
{
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

        PlatformTremble platform;
        if(hit.transform.TryGetComponent<PlatformTremble>(out platform))
            platform.ActiveRoutine();
    }
}
