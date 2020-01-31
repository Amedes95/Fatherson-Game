using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{

    public GameObject ImpactParticlePrefab;
    public static GameObject ParticleClone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            SpawnImpactParticles();
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            SpawnImpactParticles();
            collision.GetComponentInChildren<BonkableHead>().SpawnDeathParticles();
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            SpawnImpactParticles();
            Destroy(gameObject);
        }
        else if (collision.tag == "Feet")
        {
            SpawnImpactParticles();
            Destroy(gameObject);
        }
    }

    public void SpawnImpactParticles()
    {
        ParticleClone = Instantiate(ImpactParticlePrefab, transform.position, Quaternion.identity);
        Destroy(ParticleClone, 3f);
    }
}
