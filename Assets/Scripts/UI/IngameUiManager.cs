using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class IngameUiManager : MonoBehaviour
{
    #region Singleton Members
    private static IngameUiManager instance;
    public static IngameUiManager Instance => instance;
    #endregion

    [SerializeField]
    private GameObject panelUI = null;
    [SerializeField]
    private Slider mouseSensivitySlider = null;

    [Space(10)]

    [SerializeField]
    private TextMeshProUGUI lblTitle = null;
    [SerializeField]
    private TextMeshProUGUI lblGameTime = null;
    [SerializeField]
    private TextMeshProUGUI lblPropsProgress = null;

    [HideInInspector]
    public float MouseSensivity { get; private set; }

    private float gameTime = 0.0f;
    
    private void Awake()
    {
        Singleton();

        panelUI.SetActive(false);
    }

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        MouseSensivity = mouseSensivitySlider.value;

        if (PlayerProgress.Instance.IsGameOver)
        {
            EndGame();
            return;
        }

        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) && !PlayerProgress.Instance.IsGameOver)
        {
            PauseGame();
        }

        if ((int)Time.timeScale == 1)
        {
            gameTime += Time.deltaTime;
        }
    }

    private void UpdateUI()
    {
        lblGameTime.text = $"Time played: {FormatGameTime()}";
        lblPropsProgress.text = $"Scary Encounters: {PlayerProgress.Instance.GetProgress()}";
    }

    private string FormatGameTime()
    {
        int timeInSecondsInt = (int)gameTime;
        int minutes = (int)(gameTime / 60);
        int seconds = timeInSecondsInt - (minutes * 60);
        return $"{minutes.ToString("D2") + ":" + seconds.ToString("D2")}";
    }

    public void ButtonMainMenuClick()
    {
        AudioManager.Instance.StopAmbientSound("Atmo0");
        SceneManager.LoadScene("MainMenuScene");
    }

    private void PauseGame()
    {
        lblTitle.text = "Game Paused";

        if (panelUI.activeSelf)
        {
            ChangeUIState(false);
        }
        else
        {
            ChangeUIState(true);
        }
    }

    public void EndGame()
    {
        lblTitle.text = "Game End";
        ChangeUIState(true);
    }

    private void ChangeUIState(bool pauseGame)
    {
        if (pauseGame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            panelUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            panelUI.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
