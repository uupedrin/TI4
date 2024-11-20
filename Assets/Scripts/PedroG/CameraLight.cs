using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLight : MonoBehaviour
{
	[SerializeField] float time;
	[SerializeField] GameObject cameraLight;
	void Start()
	{
		InvokeRepeating("Switch", 0, time);
	}
	void Switch()
	{
		cameraLight.SetActive(!cameraLight.activeSelf);
	}
}