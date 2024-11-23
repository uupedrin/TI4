using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleEmitter : MonoBehaviour
{
	[SerializeField] ParticleSystem[] psLeft;
	[SerializeField] ParticleSystem[] psRight;
	
	PlayerController _controller;
	ParticleSystem.EmitParams[] emitParams;
	
	private void Awake()
	{
		
		for (int i = 0; i < psLeft.Length; i++)
		{
			psLeft[i].gameObject.SetActive(false);
			psRight[i].gameObject.SetActive(false);
		}
		
		_controller = GetComponent<PlayerController>();
		
		
		_controller.EmitSmoke += SmokeBurst;
	}
	
	private void OnEnable()
	{
		_controller.EmitSmoke += SmokeBurst;
	}
	
	private void OnDisable()
	{
		_controller.EmitSmoke -= SmokeBurst;
	}
	
	private void OnDestroy()
	{
		_controller.EmitSmoke -= SmokeBurst;
	}
	
	private void SmokeBurst()
	{
		ParticleSystem left = null;
		ParticleSystem right = null;
		for (int i = 0; i < psLeft.Length; i++)
		{
			if(!psLeft[i].gameObject.activeInHierarchy)
			{
				left = psLeft[i];
				break;
			}
		}
		
		for (int i = 0; i < psRight.Length; i++)
		{
			if(!psRight[i].gameObject.activeInHierarchy)
			{
				right = psRight[i];
				break;
			}
		}
		
		left.gameObject.SetActive(true);
		right.gameObject.SetActive(true);
	}
}
