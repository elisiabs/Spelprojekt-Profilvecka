using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShooterScript : MonoBehaviour
{
    [Header("Unlockables")]
    public bool Shooter1Unlocked = false;
    public bool Shooter2Unlocked = false;
    [Space]
    [Header("Components/GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pivotPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameObject backLightSprite;
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private GameObject light;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private GameObject Shooter1;
    [SerializeField] private GameObject Shooter2;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private BoxCollider2D pushCollider;
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRb;
    [Space]
    [Header("Shooter 1 Variables")]
    [SerializeField] private float recoilAmount;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float cooldownSpeed;
    [Space]
    [Header("Shooter 2 Variables")]
    [SerializeField] private float recoilAmount2;
    [SerializeField] private float knockbackAmount;
    //Private variables that do not appear in inspector:
    private float cooldown;
    private float angle;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AimShooter();
        SwitchWeapon();
        PlayerInput();

        if (cooldown < 1)
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

        if (dir.magnitude > (30 * 522f / Screen.width))
        {
            pivotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (dir.x < 0 && transform.localScale.x != -1)
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
        if (Input.GetButtonDown("Fire1") && cooldown >= 1 && Shooter1Unlocked) //Left click
        {
            Shooter1.SetActive(true);
            Shooter2.SetActive(false);
        }
        else if (Input.GetButtonDown("Fire2") && cooldown >= 1 && Shooter2Unlocked) //Right click
        {
            Shooter1.SetActive(false);
            Shooter2.SetActive(true);
        }
    }

    public void PlayerInput()
    {
        if (Input.GetButtonDown("Fire1") && cooldown >= 1 && Shooter1.activeInHierarchy && Shooter1Unlocked)
        {
            GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletVelocity;
            cooldown = 0;

            movement.knockbackForce += -new Vector2(transform.right.x, transform.right.y) * recoilAmount;

            StartCoroutine(muzzleFlash());
            animator.SetTrigger("Shoot");

            audioManager.Play("Pistol Sound");
        }
        else if (Input.GetButtonDown("Fire2") && cooldown >= 1 && Shooter2.activeInHierarchy && Shooter2Unlocked)
        {
            cooldown = 0;
            playerRb.velocity = Vector3.zero;
            movement.knockbackForce += -new Vector2(transform.right.x, transform.right.y) * recoilAmount2;

            ContactFilter2D contactFilter = new ContactFilter2D();
            List<Collider2D> hitColliders = new List<Collider2D>();
            int amountHit = pushCollider.OverlapCollider(contactFilter, hitColliders);

            if (amountHit > 0)
            {
                //något innanför pushcollider
                for (int i = 0; i < hitColliders.Count; i++)
                {
                    if(hitColliders[i].gameObject.layer == 9) //9 is the Ghost layer
                    {
                        hitColliders[i].attachedRigidbody.velocity = (new Vector2(transform.right.x, transform.right.y) * knockbackAmount);
                    }
                }
            }

            StartCoroutine(muzzleFlash());
            animator.SetTrigger("Shoot");

            audioManager.Play("Second Gun");
        }
    }

    IEnumerator muzzleFlash()
    {
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
    }
}