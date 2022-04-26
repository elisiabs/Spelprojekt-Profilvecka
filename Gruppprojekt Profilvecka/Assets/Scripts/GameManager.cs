using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;

    private Vector2 spawnPosition;

    void Start()
    {
        spawnPosition = player.transform.position;
    }

    public void Respawn()
    {
        SceneManager.LoadScene(0);
    }
}
