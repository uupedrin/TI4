using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLaser : MonoBehaviour
{
    [SerializeField] private Transform a,b;
    [SerializeField] private BoxCollider  colider;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 center = (a.position + b.position)/2;
        Vector3 size = new Vector3(Mathf.Abs(a.position.x - b.position.x),0.3f,0.3f);
        transform.position = center;
        colider.size = size;
    }

   
}
