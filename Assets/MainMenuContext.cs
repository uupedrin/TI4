using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuContext : MonoBehaviour
{
	public static MainMenuContext instance;	
	[SerializeField] TMP_Text message;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public void SetMessage(string value)
	{
		message.text = value;
	}
	
	void OnDestroy()
	{
		if(instance == this)
		{
			instance = null;
		}
	}
}
