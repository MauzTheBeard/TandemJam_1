using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlayUISound("StartGame");
        SceneManager.LoadScene("MazeLevel");
        AudioManager.Instance.PlayAmbientSoundFadeIn("Atmo0", 5.0f);
    }
}
