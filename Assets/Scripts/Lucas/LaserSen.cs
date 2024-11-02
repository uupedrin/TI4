using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserSen : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void Morrer()
    {
        SceneManager.LoadScene(sceneName);
    }
}
