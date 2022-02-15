using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pivotPoint;
    public GameObject bullet;
    public GameObject bulletSpawn;
    public RectTransform cooldownTimer;
    public float recoilAmount;
    Rigidbody2D playerRb;

    private float angle;
    public float bulletVelocity;
    public float cooldownSpeed;
    float cooldown = 0;

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

        cooldown = cooldown - (cooldownSpeed * Time.deltaTime);

        cooldownTimer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cooldown);
    }

    public void AimShooter()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pivotPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            GameObject obj = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletVelocity;
            cooldown = 75;
            playerRb.velocity = -transform.right * recoilAmount;
        }
    }
}
