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
		PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		//saveFile.postition = player.transform.position;
		saveFile.powerUpDash = player.PowerUpDash;
		saveFile.powerUpDoubleJump = player.PowerUpDoubleJump;
		saveFile.sceneName = SceneManager.GetActiveScene().name;
		//saveFile.currentSceneName = currentCheckPoint;
		saveFile.score = this.score;
		saveFile.SaveToFile(Application.persistentDataPath + "/SaveFile.json");
		Debug.Log("SavedGame");
	}

	public void LoadSavedGame()
	{
		saveFile.LoadFromFile(Application.persistentDataPath + "/SaveFile.json");
		PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		this.score = saveFile.score;
		player.transform.position = saveFile.postition;
		player.PowerUpDash = saveFile.powerUpDash;
		//currentCheckPoint = saveFile.currentSceneName;
		Debug.Log("LoadedSavedGame");
	}
}
