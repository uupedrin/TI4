using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private CinemachineFreeLook freelookCamera;
    [SerializeField] private TextMeshProUGUI sensibilidadeTexto;
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        sensitivitySlider.value = 10;
        sensitivitySlider.onValueChanged.AddListener(Sensibilidade);
        AtualizarTextoSensibilidade(sensitivitySlider.value);
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Apertou M");
            if (isPaused)
            {
                CellResume();
            }
            else
            {
                CellPause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void Pause() //altera o timescale pra 0 nao,, galera encrespou com o pause, se quiserem q volta so voltar aqui pra 0, celular/tablet continua com pause
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }
    public void CellResume()
    {
        GameManager.instance.celularMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void CellPause()
    {
        GameManager.instance.celularMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Sensibilidade(float value)
    {
        float speedA = MultiplicadorSlider(value);
        freelookCamera.m_XAxis.m_MaxSpeed = speedA;
        AtualizarTextoSensibilidade(value);
    }

    float MultiplicadorSlider(float slidervalue)
    {
        return slidervalue * 50;
    }

    void AtualizarTextoSensibilidade(float value)
    {
        sensibilidadeTexto.text = "Sensibilidade: " + value.ToString("0");
    }
}