using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    public void Reload()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
}
