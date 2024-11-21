using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
	[SerializeField] SceneField sceneToLoad;
	
	[SerializeField] SceneTrigger sceneTrigger;

	[SerializeField] Transform player;
	
	private void Awake()
	{
		sceneTrigger?.gameObject.SetActive(false);
		LoadScenes();
	}
	
	private void LoadScenes()
	{
		bool isSceneLoaded = false;
		for (int j = 0; j < SceneManager.sceneCount; j++)
		{
			Scene loadedScene = SceneManager.GetSceneAt(j);
			if(loadedScene.name == sceneToLoad.SceneName)
			{
				isSceneLoaded = true;
				break;
			}
		}
		if(!isSceneLoaded)
		{
			SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
		}
	
		if(sceneTrigger != null)
		{
			sceneTrigger.transform.position = player.position;
			sceneTrigger.gameObject.SetActive(true);
			StartCoroutine(DisableTrigger());			
		}
	}
	
	IEnumerator DisableTrigger()
	{
		yield return new WaitForSeconds(.1f);
		sceneTrigger.gameObject.SetActive(false);
	}
}
