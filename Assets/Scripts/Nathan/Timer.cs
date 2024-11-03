using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public string sceneName;  
    public float delay = 5f;  

    void Start()
    {
        
        Invoke("LoadScene", delay);
    }

    void LoadScene()
    {
        
        SceneManager.LoadScene(sceneName);
    }
}
