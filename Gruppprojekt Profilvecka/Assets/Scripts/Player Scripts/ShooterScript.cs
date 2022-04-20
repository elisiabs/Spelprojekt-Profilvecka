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
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private GameObject light;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private PlayerMovement movement;

    [Header("GameObjects used only for shooter change")]
    [SerializeField] private GameObject Shooter1;
    [SerializeField] private GameObject Shooter2;

    [Header("Components")]
    [SerializeField] private Slider slider;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Variables")]
    private float angle;
    private float cooldown;
    [Header("Shooter 1 Variables")]
    [SerializeField] private float recoilAmount;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float cooldownSpeed;
    [Header("Shooter 2 Variables")]
    [SerializeField] private float recoilAmount2;
    [SerializeField] private float knockbackAmount;

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
        if (Input.GetButtonDown("Fire1") && cooldown >= 1) //Left click
        {
            Shooter1.SetActive(true);
            Shooter2.SetActive(false);
        }
        else if (Input.GetButtonDown("Fire2") && cooldown >= 1) //Right click //TODO: implement game manager to manage if player has unlocked this yet.
        {
            Shooter1.SetActive(false);
            Shooter2.SetActive(true);
        }
    }

    public void PlayerInput()
    {
        if (Input.GetButtonDown("Fire1") && cooldown >= 1 && Shooter1.activeInHierarchy)
        {
            GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletVelocity;
            cooldown = 0;

            movement.knockbackForce += -new Vector2(transform.right.x, transform.right.y) * recoilAmount;

            StartCoroutine(muzzleFlash());
            animator.SetTrigger("Shoot");
        }
        else if (Input.GetButtonDown("Fire2") && cooldown >= 1 && Shooter2.activeInHierarchy)
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
        }
    }

    IEnumerator muzzleFlash()
    {
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(false);
    }
}