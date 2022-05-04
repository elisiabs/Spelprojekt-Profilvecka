using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
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
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Respawn()
    {
        SceneManager.LoadScene(0);
    }
}
