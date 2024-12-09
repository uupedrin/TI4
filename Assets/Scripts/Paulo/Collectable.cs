using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	public int type; 
	[SerializeField] int scoreAdd; 
	public PlayerController player; //botei teu case inteiro no trigger paulao, nao gostou vem x1 praça liberdade
	
	[SerializeField] GameObject mugMesh;
	[SerializeField] GameObject dashMesh;
	[SerializeField] GameObject jumpMesh;

	void Awake()
	{
		mugMesh.SetActive(false);
		dashMesh.SetActive(false);
		jumpMesh.SetActive(false);
		
		switch (type)
		{
			case 0:
				mugMesh.SetActive(true);
			break;
			case 1:
				jumpMesh.SetActive(true);
			break;
			case 2:
				dashMesh.SetActive(true);
			break;

		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			switch (type)
			{
				case 0:
					GameManager.instance.SumScore(scoreAdd);
					break;

				case 1:
					GameManager.instance.vitoria1 = true;
					CrossSceneReference.instance.playerController.PuloDoploAbl = true;
					GameManager.instance.skillsUI.ActiveDoubleUI();
					break;

				case 2:
					GameManager.instance.vitoria2 = true;
					CrossSceneReference.instance.playerController.rolamentoAbl = true;
					GameManager.instance.skillsUI.ActiveDashUI();
					break;
			}

			other.GetComponent<PlayerGenericSounds>().PlaySound(2);
			gameObject.SetActive(false);
		}
	}
}
