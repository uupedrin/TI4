using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
	public static GameLoader instance;
	[SerializeField] SceneField sceneToLoad;
	SceneField lastLoadedScene;
	[SerializeField] SceneTrigger sceneTrigger;

	[SerializeField] Transform player;
	Scene firstLoadedScene;
	
	public void SetScene(SceneField scene)
	{
		lastLoadedScene = sceneToLoad;
		sceneToLoad = scene;
		Reload();
	}
	
	private void Awake()
	{
		if(instance is null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		sceneTrigger?.gameObject.SetActive(false);
		LoadScenes();
	}
	
	private void OnDestroy()
	{
		if(instance == this)
		{
			instance = null;
		}
	}
	
	private void Start()
	{
		StartCoroutine(TPPlayer());
	}
	
	IEnumerator TPPlayer()
	{
		player.GetComponent<CharacterController>().enabled = false;
		yield return new WaitForSeconds(.1f);
		foreach (GameObject child in firstLoadedScene.GetRootGameObjects())
		{
			if(child.name == "SPAWN")
			{
				player.transform.position = child.transform.position;
				player.transform.rotation = child.transform.rotation;
				player.GetComponent<CharacterController>().enabled = true;
				UnloadScenes();
				break;
			}
		}
	}
	
	public void Reload()
	{
		LoadScenes();
		StartCoroutine(TPPlayer());
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
				if(firstLoadedScene.name != "Null") SceneManager.UnloadSceneAsync(firstLoadedScene);
				firstLoadedScene = SceneManager.GetSceneAt(j);
				break;
			}
		}
		if(!isSceneLoaded)
		{
			SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
			//if(firstLoadedScene.name == "Null") SceneManager.UnloadSceneAsync(firstLoadedScene);
			firstLoadedScene = SceneManager.GetSceneByName(sceneToLoad.SceneName);
		}
	}
	
	private void UnloadScenes()
	{
		if(firstLoadedScene.name != "Null") SceneManager.UnloadSceneAsync(lastLoadedScene);
	}
}
