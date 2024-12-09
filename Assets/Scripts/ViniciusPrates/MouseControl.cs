using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
	[SerializeField] GameObject light;
	[SerializeField] string message;
	
	public void OnMouseEnter()
	{
		light.SetActive(true);
		MainMenuContext.instance.SetMessage(message);
	}

	public void OnMouseExit()
	{
		light.SetActive(false);
		MainMenuContext.instance.SetMessage("");
	}
}
