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
    bool _detectandoPlayer;

    public void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        StartCoroutine(Ronda());
    }

    public IEnumerator Ronda()
    {
        while(true)
        {
            navMesh.Move(rota[_rotaPosicao].position);
            if(transform.position == rota[_rotaPosicao].position) 
            {
                _rotaPosicao++;
                if(_rotaPosicao > rota.Count)
                {
                    _rotaPosicao = 0;
                }

                //executa a animação de checar os arredores
                //ChecandoArredor();
                yield return new WaitForSeconds(1.5f);
            }
        }
    }

    public void ChecandoArredor(float time)
    {
        
    }

    public IEnumerator Perseguir(Transform player)
    {
        while(_detectandoPlayer)
        {
            navMesh.Move(player.position);
        }
        yield return new WaitForSeconds(1.5f);
    }
}
