using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColisions : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        // if(col.transform.CompareTag(""))
        // {

        // }
        // if(col.transform.CompareTag(""))
        // {
            
        // }
        // if(col.transform.CompareTag(""))
        // {
            
        // }

        PlatformTremble platform;
        if(col.transform.TryGetComponent<PlatformTremble>(out platform))
            platform.ActiveRoutine();
    }
}
