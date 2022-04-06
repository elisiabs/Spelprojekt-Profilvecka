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
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            EscPauseButton.SetActive(!EscPauseButton.activeInHierarchy);
            EscPauseMenu.SetActive(!EscPauseMenu.activeInHierarchy);
        }
    }

    public void Continue()
    {
        EscPauseButton.SetActive(true);
        EscPauseMenu.SetActive(false);
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
