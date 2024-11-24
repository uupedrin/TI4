using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiscaPisca : MonoBehaviour
{
	public GameObject [] luzes;
	public float interval;
	public float timer;
	void Start()
	{
		timer = interval;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		
		if (timer <= 0f)
		{
			if(luzes[0].activeSelf != true)
			{
				foreach(var luz in luzes)
				{
					luz.SetActive(true);
					timer = interval;
				}
			}else if (luzes[0].activeSelf == true)
			{
				foreach(var luz in luzes)
				{
					luz.SetActive(false);
					timer = interval;
				}
			}
		}
	}
}