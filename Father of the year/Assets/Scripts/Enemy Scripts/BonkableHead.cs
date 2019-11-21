using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkableHead : MonoBehaviour
{
    float BonkForce; // this should match jumpForce in PlayerMovement for consistency
    bool isBonking;
    public bool Killable;
    bool isEnemy;
    //public float rotation; //only use this on springs, not enemies
    //Vector2 rotationVector;
    public GameObject DeathParticles;
    public static GameObject DeathPartclesClone;
    public float bonkTimer;

    public void Awake()
    {
        isBonking = false;
        //gameObject.transform.parent.transform.eulerAngles = new Vector3 (0, 0, rotation);
        //rotationVector = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));

        //if (gameObject.transform.parent.tag == "Enemy" || gameObject.transform.parent.tag == "Sleeper")
        //{
        //    isEnemy = true;
        //}
        //else
        //{
        //    isEnemy = false;
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {

            if (!PlayerHealth.Dead && !isBonking)
            {
                if (bonkTimer > 0)
                {
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.down * PlayerMovement.jumpForce * 90);
                }
                collision.GetComponentInParent<Rigidbody2D>().velocity =
                    new Vector2(collision.GetComponentInParent<Rigidbody2D>().velocity.x, 0);
                isBonking = true;
                collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 135);
                bonkTimer = .2f;
                if (Killable)  // kill when bonked
                {
                    SpawnDeathParticles();
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        { 
            isBonking = false;
        }
    }

    public void SpawnDeathParticles()
    {
        DeathPartclesClone = Instantiate(DeathParticles, gameObject.transform.position, Quaternion.identity);
        //DeathParticles.Play();
    }

    public void FixedUpdate()
    {
        if (bonkTimer > 0)
        {
            bonkTimer -= Time.smoothDeltaTime;
        }
    }
}
