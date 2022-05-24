using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public AudioManager audioManager;

    public static GameManager Instance;

    void Awake() // Slightly stolen :)
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
        Time.timeScale = 1;

        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = GetComponent<Player>();
        player.OnEnable();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
