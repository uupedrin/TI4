using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Slider sensitivitySlider;
    public Sensibilidade mouseLook;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        sensitivitySlider.value = mouseLook.mouseSensitivity;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        mouseLook.mouseSensitivity = sensitivitySlider.value;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
