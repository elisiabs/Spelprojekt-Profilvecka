using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject gameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.player.playerHealth.health <= 0)
        {
            StartCoroutine(GameOver(0.2f));
        }
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

    IEnumerator GameOver(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
