using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;


    void Start()
    {
        
    }
    void Update()
    {
        if(health >= 0)
        {
            //die
        }
    }
    public void damagePlayer(float damage)
    {
        health -= damage;
    }
}
