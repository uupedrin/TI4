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
        StartCoroutine(Ronda());
    }

    public IEnumerator Ronda()
    {
        navMesh.Move(rota[_rotaPosicao].position);
        while(true)
        {
            if(transform.position == rota[_rotaPosicao].position) 
            {
                _rotaPosicao++;
                if(_rotaPosicao >= rota.Count)
                {
                    _rotaPosicao = 0;
                }

                //executa a animação de checar os arredores
                ChecandoArredor();
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void ChecandoArredor()
    {
        if(_detectandoPlayer)
        {
            StopCoroutine(Ronda());
            StartCoroutine(Perseguir(player));
        }
    }

    public IEnumerator Perseguir(Transform player)
    {
        while(_detectandoPlayer)
        {
            navMesh.Move(player.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
