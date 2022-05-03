using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public PlayerMovement player;
    public ShooterScript shooter;
    public GameObject EscPauseMenu;
    public GameObject EscPauseButton;

    // Start is called before the first frame update
    void Start()
    {
        shooter = FindObjectOfType<ShooterScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isPaused = Time.timeScale == 0;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                Pause();
            }
            else
            {
                Continue();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        EscPauseButton.SetActive(false);
        EscPauseMenu.SetActive(true);
        shooter.canShoot = false;

    }

    public void Continue()
    {
        EscPauseButton.SetActive(true);
        EscPauseMenu.SetActive(false);
        Time.timeScale = 1;
        shooter.canShoot = true;

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
