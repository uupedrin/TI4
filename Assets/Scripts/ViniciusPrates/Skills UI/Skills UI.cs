using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUI : MonoBehaviour
{
	[Header("Dash")]
	[SerializeField] GameObject dashImage;
	[SerializeField] GameObject dashSprite;
	[SerializeField] GameObject dashMesh;
	
	[Header("Double Jump")]
	[SerializeField] GameObject doubleJumpImage;
	[SerializeField] GameObject doubleJumpSprite;
	[SerializeField] GameObject doubleJumpMesh;

	public void Start()
	{
		GameManager.instance.skillsUI = this;
	}
	
	public void ActiveDoubleUI()
	{
		CrossSceneReference.instance.playerController.GetComponent<PlayerGenericSounds>().PlaySound(3);
		doubleJumpImage.SetActive(true);
		doubleJumpSprite.SetActive(true);
		doubleJumpMesh.SetActive(true);
	}

	public void ActiveDashUI()
	{
		CrossSceneReference.instance.playerController.GetComponent<PlayerGenericSounds>().PlaySound(3);
		dashImage.SetActive(true);
		dashSprite.SetActive(true);
		dashMesh.SetActive(true);
	}
}
