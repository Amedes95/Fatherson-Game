﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponentInChildren<BonkableHead>().SpawnDeathParticles();
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Feet")
        {
            Destroy(gameObject);
        }
    }
}
