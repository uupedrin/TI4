using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamentoInimigo : MonoBehaviour
{
    #region Corpo Inimigo
    [Header("Corpo Inimigo")]
    [SerializeField] Transform corpo;
    [SerializeField] NavMeshAgent navMesh;
    [SerializeField] float velocidadePerseguicao;
    [SerializeField] float velocidadePadrao = 3;
    #endregion

    #region Rota e alvo
    [Header("Rota e Alvo")]
    [SerializeField] Transform alvo;
    [SerializeField] Transform player;
    [SerializeField] List<Transform> rota;
    int _rotaPosicao = 0;
    [SerializeField] bool _detectandoPlayer;
    #endregion

    public void Start()
    {
        alvo = rota[_rotaPosicao];
        Ronda();
    }

    public void SetDetectar(bool valor)
    {
        _detectandoPlayer = valor;
    }

    public void FixedUpdate()
    {
        if((alvo.position - corpo.position).magnitude < 1f)
        {
            Debug.Log("trocando alvo");
            _rotaPosicao++;
            if(_rotaPosicao >= rota.Count)
            {
                _rotaPosicao = 0;
            }
            ChecandoArredor();
        }
    }

    public void Ronda()
    {
        navMesh.SetDestination(alvo.position);
    }

    public void ChecandoArredor()
    {
        //comportamento de checar
        Debug.Log("Checando");
        if(_detectandoPlayer)
        {
            alvo = player;
            navMesh.speed = velocidadePerseguicao;
            StartCoroutine(Perseguir());
        }
        else {
            alvo = rota[_rotaPosicao];
            navMesh.speed = velocidadePadrao;
        }
        Ronda();
    }

    public void SetAlvo(Transform alvo)
    {
        this.alvo = alvo;
    }

    public IEnumerator Perseguir()
    {
        while(_detectandoPlayer)
        {
            Ronda();
            yield return new WaitForSeconds(1f);
            if((player.position - corpo.position).magnitude < 5f)
            {
                _detectandoPlayer = false;
            }
        }

        ChecandoArredor();
    }
}
