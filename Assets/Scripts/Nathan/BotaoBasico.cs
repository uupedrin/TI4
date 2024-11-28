using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoBasico : MonoBehaviour
{
    public string sceneName;  

    
    public void ChangeScene()
    {
        GameManager.instance.Save();
        SceneManager.LoadScene(sceneName);
    }
}