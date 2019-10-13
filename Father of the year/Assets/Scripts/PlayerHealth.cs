﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameObject deathParticles;
    public static bool Dead; // it's static because I refence it directly from the playermovement && and death canvas script

    void Awake() // we don't start dead when the scene loads now do we?
    {
        deathParticles = GameObject.FindGameObjectWithTag("DeathParticle");
        Dead = false;
        deathParticles.SetActive(false);
    }

    public void KillPlayer() // Kills player
    {
        Dead = true; // oof
        deathParticles.transform.position = gameObject.transform.position;
        deathParticles.SetActive(true);
        gameObject.SetActive(false);
    }
}
