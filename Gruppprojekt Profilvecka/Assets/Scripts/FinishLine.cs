using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private Transform playerCam;

    private void Start()
    {
        playerCam = GameManager.Instance.player.playerHealth.cameraAnimator.gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Vector3 cameraPos = playerCam.position;
            playerCam.GetComponent<Animator>().enabled = false;
            playerCam.SetParent(null);
            playerCam.position = cameraPos ;
            StartCoroutine(waitLoad());
        }
    }
    IEnumerator waitLoad()
    {
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(0);
    }
}
