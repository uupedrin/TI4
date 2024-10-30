using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour
{
    [SerializeField] float distanciaMaxima = 10f;  // Distância máxima que o objeto irá percorrer
    [SerializeField] float velocidade = 5f;        // Velocidade de movimento do objeto
    [SerializeField] bool frente;
    [SerializeField] bool subir;
    private Vector3 pontoInicial;        // Ponto inicial do objeto

    private void Start()
    {
        // Salva a posição inicial do objeto
        pontoInicial = transform.position;
    }

    private void FixedUpdate()
    {
        // Calcula a distância entre o ponto inicial e a posição atual
        float distanciaPercorrida = Vector3.Distance(pontoInicial, transform.position);

        // Se a distância percorrida for menor que a distância máxima, move o objeto para frente
        if (distanciaPercorrida < distanciaMaxima && frente)
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
        }
        else if (distanciaPercorrida < distanciaMaxima && subir)
        {
            transform.Translate(Vector3.up * velocidade * Time.deltaTime);
        }
        else if (frente || subir)
        {
            // Retorna o objeto para o ponto inicial
            pontoInicial = transform.position;
            velocidade *= -1;
        }
    }
}
