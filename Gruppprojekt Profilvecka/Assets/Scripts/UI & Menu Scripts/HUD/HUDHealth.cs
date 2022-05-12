using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealth : MonoBehaviour
{
    private GameManager gameManager;

    private float currentHealth;
    public int maxHealth;
    [SerializeField] private Vector2 rPosition;
    private GameObject parent;
    

    private Image[] hearts;
    [SerializeField] private Sprite heart;
    [SerializeField] private Sprite brokenHeart;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        currentHealth = gameManager.player.playerHealth.health;
        parent = gameObject;

        initializeHearts(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeHearts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject objToSpawn = new GameObject("Empty");
            GameObject obj = Instantiate(objToSpawn);
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<RectTransform>();
            
            RectTransform rTransform = obj.GetComponent<RectTransform>();
            rTransform.localScale = new Vector3(1, 1, 1);
            rTransform.position = rPosition;
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 43.75f);
            rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 43.75f);
            
            Image image = obj.AddComponent<Image>();
            image.sprite = heart;

            shit(rTransform);
        }
    }

    IEnumerator shit(RectTransform rTransform)
    {
        yield return new WaitForSeconds(0.1f);
        rTransform.position = rPosition;
    }
}
