using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EscapeButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ShooterScript shooterScript;
    void Start()
    {
        shooterScript = FindObjectOfType<ShooterScript>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(shooterScript != null)
        {
            shooterScript.canShoot = false;
            Debug.Log("pointer escape thing");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (shooterScript != null)
        {
            shooterScript.canShoot = true;
        }
    }
}
