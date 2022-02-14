using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : MonoBehaviour
{
    public Vector3 worldPosition;
    public GameObject bullet;

    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AimShooter();
        PlayerInput();
    }

    public void AimShooter()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left button");
            Instantiate(bullet, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        }
    }
}
