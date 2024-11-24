using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class SceneTrigger : MonoBehaviour
{
	[SerializeField] SceneField _sceneToLoad;
	[SerializeField] SceneField[] _scenesToUnload;
	
	private void OnTriggerEnter(Collider other)
	{
		LoadScenes();
		UnloadScenes();
	}
	
	// private void LoadScenes()
	// {
	// 	for (int i = 0; i < _scenesToLoad.Length; i++)
	// 	{
	// 		bool isSceneLoaded = false;
	// 		for (int j = 0; j < SceneManager.sceneCount; j++)
	// 		{
	// 			Scene loadedScene = SceneManager.GetSceneAt(j);
	// 			if(loadedScene.name == _scenesToLoad[i].SceneName)
	// 			{
	// 				isSceneLoaded = true;
	// 				break;
	// 			}
	// 		}
	// 		if(!isSceneLoaded)
	// 		{
	// 			SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
	// 		}
	// 	}
	// }
	
	private void LoadScenes()
	{
		GameLoader.instance.SetScene(_sceneToLoad);
	}
	
	private void UnloadScenes()
	{
		for (int i = 0; i < _scenesToUnload.Length; i++)
		{
			for (int j = 0; j < SceneManager.sceneCount; j++)
			{
				Scene loadedScene = SceneManager.GetSceneAt(j);
				if(loadedScene.name == _scenesToUnload[i].SceneName)
				{
					SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
					break;
				}
			}
		}
	}
}
