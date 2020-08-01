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

    public bool Stunable;

    public bool DeactivateNotDestroy;

    public static int EnemiesKilled;



    public void Awake()
    {
        CurrentlyBonking = false;
        CurrentHP = MaxHP;
        EnemiesKilled = PlayerPrefs.GetInt("EnemiesKilled");
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
                    PlayerMovement.BounceBuffer = .1f; // new 3/22/20
                    PlayerMovement.JustBounced = true;
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
                            gameObject.GetComponentInParent<HopperSFX>().PlayHurtSound();
                            if (CurrentHP <= 0) // when out of health, die
                            {
                                SpawnDeathParticles();
                            }
                            // trigger different attacks at different health points
                            if (CurrentHP == MaxHP - 6)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");
                            }
                            if (CurrentHP == MaxHP - 14)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");

                            }
                            if (CurrentHP <= MaxHP - 22)
                            {
                                gameObject.GetComponentInParent<Animator>().SetBool("Hopping", false);
                                gameObject.GetComponentInParent<Animator>().SetTrigger("Roar");

                            }
                        }
                        else
                        {
                            SpawnDeathParticles();
                            PlayerPrefs.SetInt("EnemiesKilled", EnemiesKilled  += 1);
                        }
                        /// Unlocks Bonk! Achievement
                        if (PlayerPrefs.GetInt("Bonk!") == 0 && EnemiesKilled >= 20)
                        {
                            PlayerPrefs.SetInt("Bonk!", 1);
                            Debug.Log("Bonk! Unlocked");
                            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                            BGMusic.UnlockCheevo("Bonk!");
                        }
                        /// Unlocks Bonk Expert Achievement
                        if (PlayerPrefs.GetInt("Bonk Expert") == 0 && EnemiesKilled >= 60)
                        {
                            PlayerPrefs.SetInt("Bonk Expert", 1);
                            Debug.Log("Bonk Expert Unlocked");
                            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                            BGMusic.UnlockCheevo("Bonk Expert");
                        }
                        /// Unlocks Absolutely Bonkers Achievement
                        if (PlayerPrefs.GetInt("Absolutely Bonkers") == 0 && EnemiesKilled >= 100)
                        {
                            PlayerPrefs.SetInt("Absolutely Bonkers", 1);
                            Debug.Log("Absolutely Bonkers Unlocked");
                            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                            BGMusic.UnlockCheevo("Absolutely Bonkers");
                        }
                        Debug.Log(EnemiesKilled);
                    }
                    else if (Stunable)
                    {
                        gameObject.GetComponentInParent<Animator>().SetTrigger("Stun");
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
  
                }
                else
                {
                    SpawnDeathParticles();
                }
            }
            if (Stunable)
            {


                if (CurrentHP == MaxHP) // after first hit, phase 1 begins
                {
                    gameObject.GetComponentInParent<IceSkeleton>().PhaseCounter = 1;
                }
                if (gameObject.GetComponentInParent<IceSkeleton>().PhaseCounter == 2)
                {
                    Debug.Log("Phase 3 beginning");
                    gameObject.GetComponentInParent<IceSkeleton>().PhaseCounter = 3;
                }
                if (CurrentHP <= 0)
                {
                    SpawnDeathParticles();
                }
                gameObject.GetComponentInParent<Animator>().SetTrigger("Stun");
                CurrentHP -= 1; // 1 bonk = 1 health lost
                Destroy(collision.gameObject);
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
        if (DeactivateNotDestroy)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
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
