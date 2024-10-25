using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamentoInimigo : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] List<Transform> rota;
    [SerializeField] NavMeshAgent navMesh;
    int _rotaPosicao;
    [SerializeField] bool _detectandoPlayer;

    public void Awake()
    {
        //navMesh = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        Ronda();
    }

    public void FixedUpdate()
    {
        if(rota[_rotaPosicao].position.x - player.position.x < (Vector3.one * 0.2f).x && 
            rota[_rotaPosicao].position.z - player.position.z < (Vector3.one * 0.2f).z)
        {
            ChecandoArredor();
        }
    }

    public void Ronda()
    {
        navMesh.SetDestination(rota[_rotaPosicao].position);
        _rotaPosicao++;
        if(_rotaPosicao >= rota.Count)
        {
            _rotaPosicao = 0;
        }
    }

    public void ChecandoArredor()
    {
        //comportamento de checar
        Debug.Log("Checando");
        if(_detectandoPlayer)
        {
            StartCoroutine(Perseguir(player));
        }
        else {
            Ronda();
        }
    }

    public IEnumerator Perseguir(Transform player)
    {
        while(_detectandoPlayer)
        {
            navMesh.SetDestination(player.position);
            yield return new WaitForSeconds(0.5f);
        }

        ChecandoArredor();
    }
}
