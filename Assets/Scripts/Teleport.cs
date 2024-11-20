using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Teleport : MonoBehaviour
{	
	[SerializeField] Transform whereToGo;
	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<CharacterController>().enabled = false;
		other.transform.position = whereToGo.transform.position;
		other.transform.rotation = whereToGo.transform.rotation;
		other.GetComponent<CharacterController>().enabled = true;
	}
}
