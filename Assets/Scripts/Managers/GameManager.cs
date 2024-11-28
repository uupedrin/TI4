using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
	public int health;
	public bool cheat;
	public bool vitoria1, vitoria2;
	[SerializeField] PlayerController player;
	[SerializeField] GameLoader loader;

	#region SaveFile
	[SerializeField] SaveFileSO saveFile;
	#endregion
    void Awake()
	{
		Debug.Log(Application.persistentDataPath);
		if(instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		//transform.SetParent(null);
		DontDestroyOnLoad(gameObject);
		if(saveFile.isSaved)
			Load();
	}

	public void Save()
	{
		saveFile.PuloDoploAbl = player.PodePuloDoploAbl;
		saveFile.Dash = player.PodeDashAbl;
		saveFile.currentScene = loader.SceneToLoad;
		saveFile.SaveToFile(Application.persistentDataPath + "/SaveFile.json");
	}

	public void Load()
	{
		saveFile.LoadFromFile(Application.persistentDataPath + "/SaveFile.json");
		player.PodePuloDoploAbl = saveFile.PuloDoploAbl;
		player.PodeDashAbl = saveFile.Dash;
		loader.SetScene(saveFile.currentScene);

	}

    public void SumScore(int add)
    {
        score += add;
    }
}
