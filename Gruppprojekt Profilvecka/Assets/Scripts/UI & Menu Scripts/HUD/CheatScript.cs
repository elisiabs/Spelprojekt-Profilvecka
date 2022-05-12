using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    public PlayerHealth player;

    public void Invincible()
    {
        player.health = 10000000;
    }
}
