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
	public PlayerController player;
	public GameLoader loader;
	public SkillsUI skillsUI;
	public GameObject celularMenuUI;

    void Awake()
	{
		//Debug.Log(Application.persistentDataPath);
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

    public void SumScore(int add)
    {
        score += add;
    }
}
