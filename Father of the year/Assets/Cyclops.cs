using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour
{
    public Transform CyclopsZone;
    public float MoveSpeed;
    public bool Walking;
    public float AttackCycleCooldown;
    float CycleCooldownCopy;
    public GameObject Player;
    public Transform ShootZone;
    public GameObject Projectile;
    public static GameObject ProjectileClone;
    public float ProjectileSpeed;

    public GameObject RockPrefab;
    public static GameObject RockClone;
    public Transform RockDropZone;
    public BunnyBoss BunnyBoss;

    int LaserCount;
    int RockCount;
    public GameObject Tears;

    public float EndTimer = 5f;
    public GameObject Portal;





    // Start is called before the first frame update
    void Start()
    {
        Walking = false;
        CycleCooldownCopy = AttackCycleCooldown; // keep a timer copy
        RockCount = 0;
        LaserCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.activeInHierarchy && BunnyBoss.dead == false)
        {
            if (Walking)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, CyclopsZone.position, MoveSpeed); // constantly move the camera to the "Current Destination"
            }
            if (gameObject.transform.position == CyclopsZone.position)
            {
                Walking = false;
                gameObject.GetComponent<Animator>().SetBool("Walk", false);
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                AttackCycleCooldown -= Time.smoothDeltaTime;
                if (AttackCycleCooldown <= 0)
                {
                    int Randint = Random.Range(0, 2);
                    if (Randint == 1 && LaserCount < 10 || RockCount >= 4)
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("Laser");

                    }
                    else if (Randint == 0 && RockCount < 4 || LaserCount >= 10)
                    {
                        gameObject.GetComponent<Animator>().SetTrigger("Rock");

                    }
                    AttackCycleCooldown = CycleCooldownCopy;
                }
            }
            RockDropZone.transform.position = new Vector2(Player.transform.position.x, RockDropZone.position.y); // moves the drop zone over the players head
        }
        if (BunnyBoss.dead)
        {
            gameObject.GetComponent<Animator>().SetBool("Crying", true);
            Tears.SetActive(true);
            EndTimer -= Time.smoothDeltaTime;
            if (EndTimer <= 0)
            {
                EndTimer = 0;
                Portal.transform.position = Player.transform.position;
            }

        }
        else
        {
            Tears.SetActive(false);
        }
    }
        

    public void SpawnRock()
    {
        RockClone = Instantiate(RockPrefab, RockDropZone.position, Quaternion.identity);
        RockCount += 1;
        LaserCount = 0;
        Debug.Log("rock" + RockCount);
    }

    public void Shoot()
    {
        Vector3 diff = Player.transform.position - ShootZone.position;
        diff.Normalize();

        ProjectileClone = Instantiate(Projectile, ShootZone.position, Quaternion.identity);
        Destroy(ProjectileClone, 6f);
        ProjectileClone.GetComponent<Rigidbody2D>().velocity = diff * ProjectileSpeed;
        LaserCount += 1;
        RockCount = 0;
        Debug.Log("laser" + LaserCount);
    }
}
