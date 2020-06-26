﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkeleton : MonoBehaviour
{

    public Animator ChargeEffect;

    bool Firing;
    public float ProjectileSpeed;
    public float FireDelay;
    float FireDelayCopy;
    public GameObject IceProjectile;
    public static GameObject ProjectileClone;
    public Transform FireZone;
    public int PhaseCounter;
    public stalactite[] Stalactites;
    public List<GameObject> EnemyWave1;
    public GameObject PortalSpawnEffect;
    public static GameObject PortalClone;
    GameObject Player;
    float FaceDirection;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PhaseCounter = 0;
        Firing = false;
        FireDelayCopy = FireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Firing)
        {
            FireDelay -= Time.smoothDeltaTime;
        }


        if (FireDelay <= 0)
        {
            SpawnProjectile();
            FireDelay = FireDelayCopy;
        }
        if (PhaseCounter == 1)
        {
            gameObject.GetComponent<Animator>().SetBool("Phase1", true);
        }
        if (EnemyWave1.Count <= 0)
        {
            PhaseCounter = 2;
            gameObject.GetComponent<Animator>().SetBool("Phase1", false);
            gameObject.GetComponent<Animator>().SetTrigger("Thaw");

        }
        if (Player.transform.position.x < transform.position.x) // if the player is to the left of the boss
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            FaceDirection = -1;
        }
        else if (Player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            FaceDirection = 1;
        }

    }

    public void SpawnEffect()
    {
        ChargeEffect.SetTrigger("Effect");
    }

    public void SpawnProjectile()
    {
        Vector2 FireDirection = new Vector2(FaceDirection, 0);
        ProjectileClone = Instantiate(IceProjectile, FireZone.position, IceProjectile.transform.rotation);
        Destroy(ProjectileClone, 5f);
        ProjectileClone.GetComponent<Rigidbody2D>().AddForce(FireDirection * ProjectileSpeed);
        ProjectileClone.GetComponent<Rigidbody2D>().velocity = Vector2.left;
        if (FaceDirection == -1)
        {
            ProjectileClone.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            ProjectileClone.GetComponent<SpriteRenderer>().flipY = true;
        }

    }

    public void FiringWeapon()
    {
        Firing = true;
    }
    public void LowerWeapon()
    {
        Firing = false;
    }

    public void DropStalactites()
    {
        if (PhaseCounter == 1)
        {
            foreach (stalactite Stalactite in Stalactites)
            {
                Stalactite.Fall();
            }
        }

    }

    public void SpawnEnemies()
    {
        if (PhaseCounter == 1)
        {
            foreach (GameObject Enemy in EnemyWave1)
            {
                Enemy.SetActive(true);
                PortalClone = Instantiate(PortalSpawnEffect, Enemy.transform.position, Quaternion.identity);
                Destroy(PortalClone, 5f);
            }
        }

    }

    public void ClearList()
    {
        if (PhaseCounter == 1)
        {
            for (int i = 0; i < EnemyWave1.Count; i++)
            {
                if (EnemyWave1[i].activeInHierarchy == false)
                {
                    EnemyWave1.Remove(EnemyWave1[i]);
                }
            }

        }

    }
}
