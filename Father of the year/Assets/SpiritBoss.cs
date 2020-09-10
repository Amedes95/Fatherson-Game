using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBoss : MonoBehaviour
{
    Animator BossAnim;
    GameObject Player;
    Vector2 FloatDirection;
    public float FloatSpeed;
    public bool Floating;
    int SlashCounter = 10;
    int FlameCounter = 4;
    int coinCounter = 4;
    public GameObject SlashProjectilePrefab;
    public static GameObject SlashProjectilePrefabClone;
    public Transform SlashSpawnZone;
    public float ProjectileSpeed;
    public bool Summoning;
    public GameObject HomeZone;

    float OrbitaFireRate = 5f;
    public float OrbitalTravelSpeed;
    public GameObject OrbitalPrefab;
    public static GameObject OrbitalClone;

    float CoinSpawnRate = 5f;
    public GameObject CoinPrefab;
    public static GameObject CoinClone;

    public GameObject Flash;
    public static GameObject FlashClone;
    bool SightBlocked;



    // Start is called before the first frame update
    void Awake()
    {
        BossAnim = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(transform.position, Player.transform.position, Color.green);
        SightBlocked = Physics2D.Linecast(transform.position, Player.transform.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Raycasting();
        if (Player.activeInHierarchy)
        {
            if (Player.transform.position.x < transform.position.x) // if the player is to the left of the boss
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            else if (Player.transform.position.x > transform.position.x) // right of boss
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }

        }
        else // stop boss movement if player is dead
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // fireball movement
        if (Floating)
        {
            FloatAtPlayer();
        }
        // summoning
        if (Summoning)
        {
            FloatHome();
            BossAnim.SetBool("Summoning", true);
            Floating = false;
        }
        else
        {
            BossAnim.SetBool("Summoning", false);
        }

    }


    public void Wakeup() // only called during initial boss trigger
    {
        BossAnim.SetTrigger("Wake");
    }

    public void FloatAtPlayer() // float towards player's height
    {
        if (Player.activeInHierarchy)
        {
            Vector3 diff = Player.transform.position - transform.position;
            diff.Normalize();
            //FloatDirection = new Vector2(Player.transform.position.x, Player.transform.position.y) * FloatSpeed;
            BossAnim.SetBool("Floating", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, diff.y) * FloatSpeed;
            Debug.Log(diff);
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 1)
            {
                if (SlashCounter > 1)
                {
                    BossAnim.SetTrigger("Slash");
                    Summoning = false;
                }
                else
                {
                    Summoning = true; // out of slashes
                }
            }

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Floating", false);
        }

    }

    public void FloatHome() // float towards home
    {
        float speed = 2f;
        float step = speed * Time.deltaTime;

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = Vector2.MoveTowards(transform.position, HomeZone.transform.position, step);
        if (transform.position == HomeZone.transform.position)
        {
            OrbitaFireRate -= Time.smoothDeltaTime;
            CoinSpawnRate -= Time.smoothDeltaTime;
            if (OrbitaFireRate <= 0 && FlameCounter > 1)
            {
                OrbitaFireRate = 5f;
                FlameCounter -= 1;
                OrbitalClone = Instantiate(OrbitalPrefab, transform.position, Quaternion.identity);
                Destroy(OrbitalClone, 10.5f);
            }
            else if (FlameCounter <= 1 && coinCounter <= 1)
            {
                Summoning = false;
                Floating = true;
            }

            if (CoinSpawnRate <= 0 && coinCounter > 1)
            {
                CoinSpawnRate = 5f;
                coinCounter -= 1;
                CoinClone = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
                Destroy(CoinClone, 10.5f);
            }
        }
        else
        {
            OrbitaFireRate = 0;
        }
    }


    public void Slash()
    {
        RefreshCoinsNSaws();
        if (SlashCounter > 0)
        {
            SlashCounter -= 1;

            // instantiate projectile here
            SlashProjectilePrefabClone = Instantiate(SlashProjectilePrefab, SlashSpawnZone.position, Quaternion.identity);
            Destroy(SlashProjectilePrefabClone, 4f);

            Vector3 Distance = Player.transform.position - SlashProjectilePrefabClone.transform.position;
            Distance.Normalize();
            SlashProjectilePrefabClone.GetComponent<Rigidbody2D>().velocity = Distance * ProjectileSpeed;

            float rot_Z = Mathf.Atan2(Distance.y, Distance.x) * Mathf.Rad2Deg;
            SlashProjectilePrefabClone.transform.rotation = Quaternion.Euler(0f, 0f, rot_Z);

        }
    }

    public void RefreshSlashes()
    {
        SlashCounter = 10;
    }

    public void RefreshCoinsNSaws()
    {
        coinCounter = 4;
        FlameCounter = 4;
        CoinSpawnRate = 2.5f;
    }

    public void SpawnFlash()
    {
        FlashClone = Instantiate(Flash, transform.position, Quaternion.identity);
        Destroy(FlashClone, 3f);
    }

    public void PlayNoise()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

}
