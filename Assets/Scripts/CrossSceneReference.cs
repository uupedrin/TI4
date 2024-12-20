using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneReference : MonoBehaviour
{
	public static CrossSceneReference instance;
	
	public PlayerController playerController;
	public FadeOUT fadeOUT;
	
	private void Awake()
	{
		if(instance is null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
