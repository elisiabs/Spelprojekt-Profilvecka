using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private ShooterScript shooter;
    private GameManager gameManager;
    public GameObject EscPauseMenu;
    public GameObject EscPauseButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
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

    public void LoadMenu()
    {
        gameManager.LoadMenu();
    }

    public void ReloadScene()
    {
        gameManager.ReloadScene();
    }

    public void QuitGame()
    {
        gameManager.QuitGame();
    }
}
