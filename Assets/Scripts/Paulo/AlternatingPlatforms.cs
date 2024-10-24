using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingPlatforms : MonoBehaviour
{
    public GameObject[] platformGroup;
    public Material activeMaterial;
    public Material inactiveMaterial;
    int currentGroup;

    void Start()
    {
        currentGroup = 0;
        for(int i = 0; i < platformGroup[0].transform.childCount; i++)
        {
            PlatformTreatment(0, i, true, activeMaterial);
        }  
        for(int i = 1; i < platformGroup.Length; i++)
        {
            for(int j = 0; j < platformGroup[i].transform.childCount; j++)
            {
                PlatformTreatment(i, j, false, inactiveMaterial);
            }  
        }
    }

    public void Alternate()
    {
        for(int i = 0; i < platformGroup[currentGroup].transform.childCount; i++)
        {
            PlatformTreatment(currentGroup, i, false, inactiveMaterial);
        }
        currentGroup++;
        currentGroup %= platformGroup.Length;
        for(int i = 0; i < platformGroup[currentGroup].transform.childCount; i++)
        {
            PlatformTreatment(currentGroup, i, true, activeMaterial);
        }        
    }

    public void PlatformTreatment(int group, int position, bool activity, Material mat)
    {
        platformGroup[group].transform.GetChild(position).GetChild(0).GetComponent<Renderer>().material = mat;
        platformGroup[group].transform.GetChild(position).GetComponent<BoxCollider>().enabled = activity;
        platformGroup[group].transform.GetChild(position).GetComponent<Rigidbody>().isKinematic = !activity;
    }
}
