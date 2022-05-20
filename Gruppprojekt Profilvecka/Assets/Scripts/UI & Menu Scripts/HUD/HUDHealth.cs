using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUDHealth : MonoBehaviour
{
    private GameManager gameManager;

    private float currentHealth;
    [SerializeField]public int maxHealth;
    private float lastFrameHealth;
    private float lastHealthDifference;
    private Vector2 rPosition;
    private GameObject parent;
    [SerializeField] private float healthY;
    [SerializeField] private float healthXSpace;

    private Image[] hearts;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private Sprite brokenHeartSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        maxHealth = (int)gameManager.player.playerHealth.health;
        currentHealth = gameManager.player.playerHealth.health;
        parent = gameObject;

        rPosition = new Vector2(Screen.width/2, healthY);
        hearts = new Image[maxHealth];

        InitializeHearts();
    }

    private void Update()
    {
        currentHealth = gameManager.player.playerHealth.health;
        lastHealthDifference = lastFrameHealth - currentHealth;
        if (currentHealth < lastFrameHealth)
        {
            DamageHearts((int)lastFrameHealth - (int)currentHealth);
            Debug.Log("Health lowered");
        }
        lastFrameHealth = gameManager.player.playerHealth.health;
    }

    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            float desiredY = (rPosition.x + (healthXSpace * (i + 1)));
            Vector2 desiredPosition = new Vector2(desiredY, rPosition.y);

            GameObject objToSpawn = new GameObject("Empty");
            GameObject obj = Instantiate(objToSpawn);
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<RectTransform>();
            
            RectTransform rTransform = obj.GetComponent<RectTransform>();
            rTransform.localScale = new Vector3(1, 1, 1);
            rTransform.position = desiredPosition;
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 43.75f);
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 43.75f);
            
            Image image = obj.AddComponent<Image>();
            image.sprite = heartSprite;
            hearts[i] = image;

        }
    }

    public void DamageHearts(int damage)
    {
        for (int i = hearts.Length - 1; i >= currentHealth - damage; i--)
        {
            Debug.Log("Damaged hearts. i = " + i);

            hearts[i].sprite = brokenHeartSprite;
        }
    }
}
