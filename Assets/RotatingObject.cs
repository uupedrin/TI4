using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
	[SerializeField] float rotationSpeed = 50;
	[SerializeField] Vector3 axis;
	void Update()
	{
		transform.Rotate(axis * rotationSpeed * Time.deltaTime);
	}
}
