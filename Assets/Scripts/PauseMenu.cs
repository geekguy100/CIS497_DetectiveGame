using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;

    private void OnEnable()
    {
        EventManager.OnGameRestart += UnPause;
    }

    private void OnDisable()
    {
        EventManager.OnGameRestart -= UnPause;
    }


    private void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void UnPause()
    {
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMenu()
    {
        UnPause();
        EventManager.OnGameRestart();
        SceneManager.LoadScene("Main Menu");
    }


}
