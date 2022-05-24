using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDHealth : MonoBehaviour
{
    private GameManager gameManager;

    private float currentHealth;
    [HideInInspector]public int maxHealth;
    private float lastFrameHealth;
    private float lastHealthDifference;
    private GameObject parent;

    private Image[] hearts;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite brokenHeartSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        maxHealth = (int)gameManager.player.playerHealth.health;
        currentHealth = gameManager.player.playerHealth.health;
        gameManager.player.playerHealth.OnPlayerDamaged.AddListener(DamageHearts);
        parent = gameObject;

        hearts = new Image[maxHealth];

        InitializeHearts();
    }


    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject objToSpawn = new GameObject("Empty");
            GameObject obj = Instantiate(objToSpawn);
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<RectTransform>();
            
            RectTransform rTransform = obj.GetComponent<RectTransform>();
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 43.75f);
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 43.75f);
            
            Image image = obj.AddComponent<Image>();
            image.sprite = heartSprite;
            hearts[i] = image;

        }
    }

    public void DamageHearts(int damage)
    {
        if(hearts != null)
        {
            currentHealth -= damage;
            for (int i = hearts.Length - 1; i > currentHealth - damage; i--)
            {
                Debug.Log("Damaged hearts. i = " + i);

                hearts[i].sprite = brokenHeartSprite;
            }
        }
    }
}
