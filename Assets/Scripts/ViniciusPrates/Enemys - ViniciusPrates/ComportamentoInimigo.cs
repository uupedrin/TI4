using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ComportamentoInimigo : MonoBehaviour
{
	[SerializeField] Transform pontoA, pontoB, corpo;
	Vector3 dir;
	[SerializeField] float speed;
	float pos;
	[SerializeField] [Range(0, 1)] float posInicial;

	public void FixedUpdate()
	{
		if (pontoA != null)
		{
			pos = Mathf.PingPong(posInicial + Time.time * speed, 1);
			Ronda(pontoA, pontoB, pos);
		}
	}

	public void Ronda(Transform pontoA, Transform pontoB, float x)
	{
		dir.x = Mathf.Lerp(pontoA.position.x, pontoB.position.x, x);
		dir.z = Mathf.Lerp(pontoA.position.z, pontoB.position.z, x);
		dir.y = corpo.position.y;
		corpo.position = dir;
	}
}
