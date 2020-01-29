using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkableHead : MonoBehaviour
{
    float BonkForce; // this should match jumpForce in PlayerMovement for consistency
    public static bool CurrentlyBonking;
    public bool Killable;
    bool isEnemy;
    //public float rotation; //only use this on springs, not enemies
    //Vector2 rotationVector;
    public GameObject DeathParticles;
    public static GameObject DeathPartclesClone;
    public float bonkTimer;
    public bool OnTrampoline;
    public int CurrentHP;
    public int MaxHP;


    public void Awake()
    {
        CurrentlyBonking = false;
        CurrentHP = MaxHP;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            if (!PlayerHealth.Dead && !CurrentlyBonking)
            {
                if (OnTrampoline == false)
                {
                    //collision.GetComponentInParent<Animator>().SetBool("DoubleJumpActive", true);
                    collision.GetComponentInParent<Animator>().SetTrigger("Jump");
                    //Debug.Log("Bonk");
                    Boombox.SetVibrationIntensity(.1f, .25f, .75f);
                    if (bonkTimer > 0)
                    {
                        collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.down * PlayerMovement.jumpForce * 90);
                    }
                    collision.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(collision.GetComponentInParent<Rigidbody2D>().velocity.x, 0);
                    CurrentlyBonking = true;
                    collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 150);
                    bonkTimer = .2f;

                    if (Killable)  // kill when bonked or reduce health
                    {
                        if (gameObject.transform.parent.tag == "Boss") // for hopper boss
                        {
                            if (CurrentHP == MaxHP) // is it the first bonk?
                            {
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Wake"); // awaken
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", true); // begin attacking
                            }
                            CurrentHP -= 1; // 1 bonk = 1 health lost
                            if (CurrentHP <= 0) // when out of health, die
                            {
                                SpawnDeathParticles();
                            }
                            // trigger different attacks at different health points
                            if (CurrentHP == MaxHP - 10)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");
                            }
                            if (CurrentHP == MaxHP - 20)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");

                            }
                            if (CurrentHP <= MaxHP - 30)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");

                            }
                        }
                        else
                        {
                            SpawnDeathParticles();
                        }
                    }
                }
                else
                {
                    SpawnDeathParticles();
                }

            }
        }
        if (collision.tag == "Boss") // if the boss touches an enemy
        {
            if (Killable)
            {
                SpawnDeathParticles();
            }
        }
        if (collision.tag == "Stalactite") // if a stalactite touches an enemy
        {
            if (Killable)
            {
                if (gameObject.transform.parent.tag == "Boss")
                {
                    //CurrentHP -= 1; // 1 bonk = 1 health lost
                    //if (CurrentHP <= 0)
                    //{
                    //    SpawnDeathParticles();
                    //}
                }
                else
                {
                    SpawnDeathParticles();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            CurrentlyBonking = false;
        }
    }

    public void SpawnDeathParticles()
    {
        DeathPartclesClone = Instantiate(DeathParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
        Destroy(DeathPartclesClone, 3f); // kill after 3 seconds
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
