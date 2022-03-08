using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pivotPoint;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public Slider slider;
    public GameObject backLightSprite;
    public Animator animator;
    public float recoilAmount;
    Rigidbody2D playerRb;

    private float angle;
    public float bulletVelocity;
    public float cooldownSpeed;
    float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AimShooter();
        PlayerInput();
        
        if(cooldown < 1)
        {
            cooldown = cooldown + (cooldownSpeed * Time.deltaTime);
            backLightSprite.SetActive(false);
        }
        else
        {
            cooldown = 1;
            backLightSprite.SetActive(true);
        }

        slider.value = cooldown;
    }

    public void AimShooter()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if(dir.magnitude > 30)
        {
            pivotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        if(dir.x < 0 && transform.localScale.x != -1)
        {
            transform.localScale = new Vector2(1, -1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0) && cooldown >= 1)
        {
            GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletVelocity;
            cooldown = 0;
            playerRb.velocity = -transform.right * recoilAmount;
            animator.SetTrigger("Shoot");
        }
    }
}
