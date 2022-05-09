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
    [SerializeField] private AnimationClip imageFadeIn;
    [SerializeField] private AnimationClip imageFadeOut;
    [SerializeField] private AnimationClip textFadeIn;
    [SerializeField] private AnimationClip textFadeOut;

    private ShooterScript shooterScript;
    private bool catClose = false;

    private bool showWalk = true;
    private bool showJump = true;
    private bool hideJump = false;
    private bool showAim = true;
    private bool hideShooter1 = false;
    private bool showShooter2 = false;

    // Start is called before the first frame update
    void Start()
    {
        cat = FindObjectOfType<CatTutorial>();
        shooterScript = GameManager.Instance.player.shooterscript;
    }

    // Update is called once per frame
    void Update()
    {
        if (cat.checkPlayerClose())
        {
            catClose = true;
        }

        if (showWalk)
        {
            if (GameManager.Instance.player.playermovement.transform.position.y <= 3 && !catClose)
            {
                FadeInAnimation(walkPrompt.transform);
                showWalk = false;
            }
        }
        else if (catClose && !hideJump)
        {
            hideJump = true;
            //Debug.Log("catclose");
            showWalk = false;
            FadeOutAnimation(walkPrompt.transform);
        }
        
        switch (cat.step)
        {
            case 1:
                if (showJump)
                {
                    FadeInAnimation(jumpPrompt.transform);
                    showJump = false;
                }
                break;
            case 2:
                showJump = true;
                break;
            case 3:
                if (showJump)
                {
                    FadeOutAnimation(jumpPrompt.transform);
                    showJump=false;
                }
                break;
        }

        if (showAim)
        {
            if (shooterScript.Shooter1Unlocked)
            {
                FadeInAnimation(aimPrompt.transform);
                StartCoroutine(hideAim(3f));
                showAim = false;
            }
        }


        if (!hideShooter1)
        {
            if (shooterScript.Shooter1Unlocked && shooterScript.canShoot && Input.GetButtonDown("Fire1"))
            {
                FadeOutAnimation(shooter1Prompt.transform);
                hideShooter1 = true;
            }
        }
        /*
        if (GameManager.Instance.player.shooterscript.Shooter2Unlocked)
        {
            shooter1Prompt.SetActive(false);
            shooter2Prompt.SetActive(true);
            FadeInAnimation(shooter2Prompt.transform);
        }
        if (GameManager.Instance.player.shooterscript.Shooter2Unlocked && Input.GetButtonDown("Fire2"))
        {
            fire2 = true;
        }
        if (fire2)
        {
            FadeOutAnimation(shooter2Prompt.transform);
        }
        */
    }
    
    IEnumerator hideAim (float seconds)
    {
        shooterScript.canShoot = false;
        yield return new WaitForSeconds(seconds);
        FadeOutAnimation(aimPrompt.transform);
        yield return new WaitForSeconds(1);
        shooterScript.canShoot = true;
        FadeInAnimation(shooter1Prompt.transform);
    }
    
    private void FadeInAnimation(Transform parent)
    {
        parent.gameObject.SetActive(true);

        foreach(Transform child in parent)
        {
            Animation anim = child.gameObject.AddComponent(typeof(Animation)) as Animation; //Add animation component.

            if (child.GetComponent<Image>() != null) //If it has Image.
            {
                anim.AddClip(imageFadeIn, "ImageFadeIn"); //Add AnimationClip.
                anim.Play("ImageFadeIn"); //Play AnimationClip.
            }
            else if (child.GetComponent<TextMeshProUGUI>() != null) //If it has TMPro.
            {
                anim.AddClip(textFadeIn, "TextFadeIn"); //Add AnimationClip.
                anim.Play("TextFadeIn"); //Play AnimationClip.
            }
        }
    }
    private void FadeOutAnimation(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Animation anim = child.gameObject.GetComponent<Animation>(); //Gets existing Animation component.

            if (child.GetComponent<Image>() != null) //If it has Image.
            {
                anim.AddClip(imageFadeOut, "ImageFadeOut"); //Add AnimationClip.
                anim.Play("ImageFadeOut"); //Play AnimationClip.
            }
            else if (child.GetComponent<TextMeshProUGUI>() != null) //If it has TMPro.
            {
                anim.AddClip(textFadeOut, "TextFadeOut"); //Add AnimationClip.
                anim.Play("TextFadeOut"); //Play AnimationClip.
            }
        }
    }
}
