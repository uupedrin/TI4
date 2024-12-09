using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraAtaque : MonoBehaviour
{
	[SerializeField] private float tempo;
	[SerializeField] private GameObject spawn;
	[SerializeField] private bool dentro, cor, volta;
	[SerializeField] private bool drone;
	private float timer = 0f;
	private Renderer targetRenderer; 
	 [SerializeField] private Material objectMaterial; 
	[SerializeField] private Color corPadrao;
	[SerializeField] private Color startColor;
	[SerializeField] private Color endColor;
	private Color aux;

	void Start()
	{
		targetRenderer = GetComponent<Renderer>();

		if (targetRenderer != null && drone)
		{
			objectMaterial = targetRenderer.material;
			objectMaterial.color = corPadrao;
		}
		else
		{
			Debug.LogWarning("Nenhum Renderer encontrado neste objeto.");
		}
	}

	void Update()
	{
		if(cor)
		{
			timer += Time.deltaTime;
			float progress = timer / tempo;
			Color currentColor = Color.Lerp(startColor, endColor, progress);
			objectMaterial.color = currentColor;
			if (progress >= 1f)
			{
				cor = false;
				timer = 0f;
			}
		}
		else if(volta)
		{
			timer += Time.deltaTime;
			float progress = timer / tempo;
			Color currentColor = Color.Lerp(aux, corPadrao, progress);
			objectMaterial.color = currentColor;
			if (progress >= 1f)
			{
				volta = false;
				timer = 0f;
			}
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if(GameManager.instance.cheat)
		return;
		
		if(!dentro)
		{
			timer = 0f;
			dentro = true;
			if(drone)
			cor = true;
			StartCoroutine(Ataque());
			volta = false;
			Debug.Log(this.gameObject.name);
		}
	}
	 void OnTriggerExit(Collider other)
	{
		timer = 0f;
		dentro = false;
		cor = false;
		if(aux!=null)
			aux = objectMaterial.color;
		volta = true;
	   
	}

	IEnumerator Ataque()
	{
		
		yield return new WaitForSeconds(tempo);
		if(dentro)
		{
			Debug.Log(":3");
			if(CrossSceneReference.instance.playerController.health > 0)
			{
				CrossSceneReference.instance.playerController.GetComponent<CharacterController>().enabled = false;
				CrossSceneReference.instance.playerController.transform.position = spawn.transform.position;
				CrossSceneReference.instance.playerController.transform.rotation = spawn.transform.rotation;
				CrossSceneReference.instance.playerController.GetComponent<CharacterController>().enabled = true;
				CrossSceneReference.instance.playerController.GetComponent<PlayerGenericSounds>().PlaySound(1);
				CrossSceneReference.instance.playerController.health--;
				dentro = false;
				
				timer = 0f;
				dentro = false;
				cor = false;
				if(aux!=null)
				{
					aux = objectMaterial.color;
				}
				volta = true;
			}
			else SceneManager.LoadScene("GameOver");
		}       
	}	
}
