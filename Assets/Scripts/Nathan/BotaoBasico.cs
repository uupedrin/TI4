using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoBasico : MonoBehaviour
{
    public string sceneName;  

    
    public void ChangeScene()
    {
        Debug.Log("BOTAOBASICO");
        GameManager.instance.SaveGame();
        SceneManager.LoadScene(sceneName);
    }
}