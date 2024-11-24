using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
	[SerializeField] SaveFileSO saveFile;
	[SerializeField] GameLoader gameLoader;
	
    void Awake()
	{
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
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SumScore(int add)
    {
        score += add;
    }

	public void SaveGame()
	{
		PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		saveFile.PuloDoploAbl = player.PuloDoploAbl;
		saveFile.Dash = player.Dash;
		saveFile.score = this.score;
		//saveFile.postition = player.transform.position;
		//saveFile.currentSceneName = currentCheckPoint;
		saveFile.SaveToFile(Application.persistentDataPath + "/SaveFile.json");
		Debug.Log("SavedGame");
		Debug.Log(Application.persistentDataPath);
	}

	public void LoadSavedGame()
	{
		saveFile.LoadFromFile(Application.persistentDataPath + "/SaveFile.json");
		PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		this.score = saveFile.score;
		player.Dash = saveFile.Dash;
		player.PuloDoploAbl = saveFile.PuloDoploAbl;
		//player.transform.position = saveFile.postition;
		//currentCheckPoint = saveFile.currentSceneName;
		Debug.Log("LoadedSavedGame");
	}
}
