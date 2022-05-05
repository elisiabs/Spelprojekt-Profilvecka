using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPrompts : MonoBehaviour
{
    [SerializeField] private CatTutorial cat;
    [SerializeField] private GameObject walkPrompt;
    [SerializeField] private GameObject jumpPrompt;
    [SerializeField] private GameObject aimPrompt;
    [SerializeField] private GameObject shooter1Prompt;
    [SerializeField] private GameObject shooter2Prompt;

    private bool startFadeIn = false;
    private bool startFadeOut = false;
    public float faded = 0;
    [SerializeField] private float fadeSpeed;
    private bool catClose = false;
    private bool showJumpPrompt = false;
    private bool notShowJumpPrompt = false;
    bool fire1 = false;
    bool fire2 = false;

    // Start is called before the first frame update
    void Start()
    {
        cat = FindObjectOfType<CatTutorial>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadeIn && faded <= 1)
        {
            faded += (fadeSpeed * Time.deltaTime);
        }
        if (startFadeOut && faded >= 0)
        {
            faded -= (fadeSpeed * Time.deltaTime);
        }
        if (cat.checkPlayerClose())
        {
            catClose = true;
        }
        if (GameManager.Instance.player.playermovement.transform.position.y <= 3 && faded <= 1 && !catClose)
        {
            walkPrompt.SetActive(true);
            fadeIn(walkPrompt.transform);
        }
        if (catClose)
        {
            fadeOut(walkPrompt.transform);
        }
        switch (cat.step)
        {
            case 1:
                walkPrompt.SetActive(false);
                jumpPrompt.SetActive(true);
                showJumpPrompt = true;
                break;
            case 3:
                notShowJumpPrompt = true;
                showJumpPrompt = false;
                break;
        }
        if (showJumpPrompt)
        {
            fadeIn(jumpPrompt.transform);
        }
        else if (notShowJumpPrompt)
        {
            fadeOut(jumpPrompt.transform);
        }

        if (GameManager.Instance.player.shooterscript.Shooter1Unlocked)
        {
            jumpPrompt.SetActive(false);
            shooter1Prompt.SetActive(true);
            fadeIn(shooter1Prompt.transform);
        }
        
        if(GameManager.Instance.player.shooterscript.Shooter1Unlocked && Input.GetButtonDown("Fire1"))
        {
            fire1 = true;
        }
        if (fire1)
        {
            fadeOut(shooter1Prompt.transform);
        }
        
        if (GameManager.Instance.player.shooterscript.Shooter2Unlocked)
        {
            shooter1Prompt.SetActive(false);
            shooter2Prompt.SetActive(true);
            fadeIn(shooter2Prompt.transform);
        }
        if (GameManager.Instance.player.shooterscript.Shooter2Unlocked && Input.GetButtonDown("Fire2"))
        {
            fire2 = true;
        }
        if (fire2)
        {
            fadeOut(shooter2Prompt.transform);
        }
    }

    private void fadeIn(Transform parent)
    {
        startFadeIn = true;
        startFadeOut = false;
        changeFaded(parent);
    }

    private void fadeOut(Transform parent)
    {
        startFadeIn = false;
        startFadeOut = true;
        changeFaded(parent);
    }

    private void changeFaded(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child != null)
            {
                if (child.GetComponent<Image>() != null)
                {
                    child.GetComponent<Image>().color = new Color(1, 1, 1, faded);
                }
                else if (child.GetComponent<TextMeshProUGUI>() != null)
                {
                    child.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, faded);
                }
            }
        }
    }
}
