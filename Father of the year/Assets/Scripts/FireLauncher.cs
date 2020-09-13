using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLauncher : MonoBehaviour
{
    public GameObject FireBallPrefab;
    public static GameObject FireBallClone;
    public Transform FireSpawn;

    public bool FireRight;
    public bool FireLeft;
    public bool FireDown;
    public bool FireUp;

    Vector2 FireDirection;

    public float ProjectileSpeed;
    public float FireRate;
    float FireRateCopy;

    public float InitialDelay;

    // Start is called before the first frame update
    void Start()
    {
        FireRateCopy = FireRate;
        FireRate = .02f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FireRight)
        {
            FireDirection = Vector2.right;
        }
        else if (FireDown)
        {
            FireDirection = Vector2.down;
        }
        else if (FireLeft)
        {
            FireDirection = Vector2.left;
        }
        else if (FireUp)
        {
            FireDirection = Vector2.up;
        }

        if (InitialDelay <= 0)
        {
            InitialDelay = 0;
            if (FireRate > 0)
            {
                FireRate -= Time.smoothDeltaTime;
            }
            else
            {
                CreateFireBall();
                FireRate = FireRateCopy;
            }
        }
        else
        {
            InitialDelay -= Time.smoothDeltaTime;
        }

    }

    public void CreateFireBall()
    {
        FireBallClone = Instantiate(FireBallPrefab, FireSpawn.position, transform.rotation);
        FireBallClone.GetComponent<Rigidbody2D>().velocity = (FireDirection * ProjectileSpeed);
    }
}
