using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterScript : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private BoxCollider2D pushCollider;
    [SerializeField] private GameObject backLightSprite;
    [SerializeField] private GameObject light;

    [Header("GameObjects used only for shooter change")]
    [SerializeField] private GameObject Shooter1;
    [SerializeField] private GameObject Shooter2;

    [Header("Components")]
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    private Rigidbody2D playerRb;

    [Header("Variables")]
    private float angle;
    private float cooldown;
    [Header("Shooter 1 Variables")]
    [SerializeField] private float recoilAmount;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float cooldownSpeed;
    [Header("Shooter 2 Variables")]
    [SerializeField] private float recoilAmount2;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AimShooter();
        SwitchWeapon();
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

    public void SwitchWeapon()
    {
        if (Input.GetMouseButtonDown(0) && cooldown >= 1) //Left click
        {
            Shooter1.SetActive(true);
            Shooter2.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1) && cooldown >= 1) //Right click //TODO: implement game manager to manage if player has unlocked this yet.
        {
            Shooter1.SetActive(false);
            Shooter2.SetActive(true);
        }
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0) && cooldown >= 1 && Shooter1.activeInHierarchy)
        {
            GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletVelocity;
            cooldown = 0;
            playerRb.velocity = -transform.right * recoilAmount;
            StartCoroutine(muzzleFlash());
            animator.SetTrigger("Shoot");
        }
        else if(Input.GetMouseButtonDown(1) && cooldown <= 1 && Shooter2.activeInHierarchy)
        {
            cooldown = 0;
            playerRb.velocity = Vector2.zero;
            playerRb.velocity = -transform.right * recoilAmount2;
            StartCoroutine(muzzleFlash());
            animator.SetTrigger("Shoot");
        }
    }

    IEnumerator muzzleFlash()
    {
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
    }

    /*private void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("lol");
        
        Debug.Log("OnTriggerStay2D");
        if (Input.GetMouseButtonDown(1) && cooldown <= 1 && Shooter2.activeInHierarchy)
        {
            Debug.Log("Player Right click while col");
            if (col.gameObject.layer == 9) //9 is layer "Ghost".
            {
                Debug.Log("Enemy pushed");
                col.gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * 6000;
            }
        }
    }
    */
}
