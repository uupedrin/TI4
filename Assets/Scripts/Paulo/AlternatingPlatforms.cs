using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingPlatforms : MonoBehaviour
{
    public void Alternate()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!transform.GetChild(i).gameObject.activeInHierarchy);
        }
    }
}
