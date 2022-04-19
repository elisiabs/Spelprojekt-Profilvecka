using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject EscPauseMenu;
    public GameObject EscPauseButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool isPaused = Time.timeScale == 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                Time.timeScale = 0;
                EscPauseButton.SetActive(false);
                EscPauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                EscPauseButton.SetActive(true);
                EscPauseMenu.SetActive(false);
            }
        }
        /*if(inPause = true && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
        }*/
    }

    public void Pause()
    {
        Time.timeScale = 0;
        EscPauseButton.SetActive(false);
        EscPauseMenu.SetActive(true);
    }

    public void Continue()
    {
        EscPauseButton.SetActive(true);
        EscPauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
