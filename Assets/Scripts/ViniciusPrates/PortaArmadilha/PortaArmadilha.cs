using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class PortaArmadilha : MonoBehaviour
{
    [SerializeField] Transform pontoInicial;
    [SerializeField] Transform pontoFinal;
    [SerializeField] Transform portaCorpo;
    enum MoveType
    {
        Horizontal,
        Vertical
    }

    [SerializeField] MoveType moveType;

    UnityEngine.Vector3 dir;
    [Range(0, 1)]
    [SerializeField]float x;


    [Range(0,20)]
    [SerializeField] float speed;
    bool movendo = true;
    float decremento = 0.1f;

    public void FixedUpdate()
    {
        x = Mathf.PingPong(Time.time * speed, 1);
        Transicao();
    }
    public void Transicao()
    {
        switch(moveType)
        {
            case MoveType.Horizontal:
                dir.x = Mathf.Lerp(pontoInicial.position.x, pontoFinal.position.x, x);
                dir.y = portaCorpo.position.y;
                dir.z = portaCorpo.position.z;
                portaCorpo.position = dir;
                break;
            case MoveType.Vertical:
                dir.y = Mathf.Lerp(pontoInicial.position.y, pontoFinal.position.y, x);
                dir.x = portaCorpo.position.x;
                dir.z = portaCorpo.position.z;
                portaCorpo.position = dir;
                break;
        }
    }
}
