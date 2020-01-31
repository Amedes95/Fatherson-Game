using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoss : MonoBehaviour
{

    public GameObject DeathParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
        {
            Destroy(collision.gameObject);
            DeathParticles.transform.position = collision.transform.position;
            DeathParticles.SetActive(true);
        }
    }
}
