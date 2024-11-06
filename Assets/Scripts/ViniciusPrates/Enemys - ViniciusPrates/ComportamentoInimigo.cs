using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamentoInimigo : MonoBehaviour
{
    #region Corpo Inimigo
    [Header("Corpo Inimigo")]
    [SerializeField] Transform corpo;
    [SerializeField] float velocidadePerseguicao;
    [SerializeField] float velocidadePadrao = 3;
    [SerializeField] float velocidade;
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
        velocidade = velocidadePadrao;
        alvo = rota[_rotaPosicao];
        Ronda();
    }

    public void SetAlvo(Transform alvo)
    {
        this.alvo = alvo;
    }

    public void SetDetectar(bool valor)
    {
        _detectandoPlayer = valor;
    }
    public void Ronda()
    {
        StartCoroutine(RondaComportamento());
    }

    public IEnumerator RondaComportamento()
    {
        while((alvo.position - corpo.position).magnitude > 2f)
        {
            corpo.position += (alvo.position - corpo.position).normalized * velocidade * Time.deltaTime;   
            _detectandoPlayer = ChecandoArredor();
            yield return new WaitForSeconds(velocidade / 100);
        }
    }

    public void FixedUpdate()
    {
        if((alvo.position - corpo.position).magnitude < 2f)
        {
            Debug.Log("trocando alvo");
            _rotaPosicao++;
            if(_rotaPosicao >= rota.Count)
            {
                _rotaPosicao = 0;
            }
            alvo = rota[_rotaPosicao];
        }
    }

    public bool ChecandoArredor()
    {
        //comportamento de checar
        Debug.Log("Checando");
        if(_detectandoPlayer)
        {
            alvo = player;
            velocidade = velocidadePerseguicao;
            StartCoroutine(Perseguir());
            return true;
        }
        else {
            velocidade = velocidadePadrao;
        }
        return false;
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
