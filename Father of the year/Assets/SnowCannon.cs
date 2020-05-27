﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCannon : MonoBehaviour
{
    public GameObject SnowBall;
    public static GameObject SnowBallClone;
    public Transform FireZone;
    public bool FireRight;
    Vector2 FireDirection;
    public float ProjectileSpeed;
    public float FireRate;
    float FireRateCopy;
    bool InRange;
    GameObject Player;
    public bool FireImmediately;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FireRateCopy = FireRate;
        //if (FireImmediately)
        //{
        //    FireRate = .001f;
        //}
    }

    public void FireSnowBall()
    {
        SnowBallClone = Instantiate(SnowBall, FireZone.position, Quaternion.identity);
        if (FireRight)
        {
            FireDirection = Vector2.right;
        }
        else
        {
            FireDirection = Vector2.left;
        }
        SnowBallClone.GetComponent<Rigidbody2D>().velocity = FireDirection * 2;
        SnowBallClone.GetComponent<Rigidbody2D>().AddForce(FireDirection * ProjectileSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Player.activeInHierarchy)
        {
            InRange = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FireRate = .001f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InRange = false;
        }
    }

    private void FixedUpdate()
    {
        if (InRange)
        {
            FireRate -= Time.smoothDeltaTime;
        }
        if (FireRate <= 0)
        {
            FireRate = FireRateCopy;
            gameObject.GetComponent<Animator>().SetTrigger("Fire");
        }
    }
}
