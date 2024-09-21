using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class Drone : MonoBehaviour
{
    [SerializeField] Transform[] pontos;
    [SerializeField] GameObject _mesa ,prefab;
    [SerializeField] float _speed;
    [SerializeField] Transform MeioSala, mesaSpawn;
    [SerializeField] private bool indo, voltando, meio;
    [SerializeField] private Transform transformAtual;
    [SerializeField] private float timeEspera;
    private float startTime;
    private Vector3 startPos;
    private int p;
    [SerializeField] private Vector3 startT;
    void Start()
    {
        startT = transform.position;
        StartCoroutine(MoveObject());
    }

    void Update()
    {


    }
    void Colocar(int p_index)
    {
        transformAtual = pontos[p_index];
        startPos = transform.position;
        meio = true;

    }

    IEnumerator MoveObject()
    {
        while (meio || indo || voltando)
        {
            if (meio)
            {
                transform.position = Vector3.MoveTowards(transform.position, MeioSala.position, _speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, MeioSala.position) < 0.01f)
                {
                    meio = false;
                    indo = true;
                    startPos = transform.position;
                    yield return new WaitForSeconds(timeEspera);
                }
            }
            else if (indo)
            {
                transform.position = Vector3.MoveTowards(transform.position, transformAtual.position, _speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, transformAtual.position) < 0.01f)
                {
                    voltando = true;
                    indo = false;
                    startPos = transform.position;
                    _mesa.transform.SetParent(null);
                    _mesa.GetComponent<Rigidbody>().useGravity = true;
                    yield return new WaitForSeconds(timeEspera);

                }
            }
            else if (voltando)
            {
                transform.position = Vector3.MoveTowards(transform.position, startT, _speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startT) < 0.01f)
                {
                    voltando = false;
                    startPos = transform.position;
                    _mesa = Instantiate(prefab, mesaSpawn.position, mesaSpawn.rotation, transform);

                }
            }
            yield return null;
        }
    }
}
