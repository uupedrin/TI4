using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    
    public void ChangeScene(string sceneName)
    {
       
        SceneManager.LoadScene(sceneName);
    }
}
