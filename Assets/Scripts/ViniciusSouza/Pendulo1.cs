using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulo1 : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float limite = 75f;
    [SerializeField] private bool randomStart = false;
    private float random = 0f ;

    private void Awake()
    {
        if (randomStart)
        {
            random = Random.Range(0f, 1f);
        }
    }

    private void Update()
    {
        float angulo = limite * Mathf.Sin(Time.time + random *  speed);
        transform.localRotation = Quaternion.Euler(0, 0, -angulo);
    }
}
