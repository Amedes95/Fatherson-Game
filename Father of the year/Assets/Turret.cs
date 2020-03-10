using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform BarrelPivot;
    GameObject Player;
    public bool InSights;
    TurretDetector Detector;
    public bool Increasing;
    Vector3 SpinAngle;
    float ShootCooldown;
    public float FireRate;

    public GameObject Projectile;
    public static GameObject ProjectileClone;
    public Transform LaunchZone;
    public float ProjectileSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //BarrelPivot.rotation = Quaternion.Euler(0f, 0f, transform.parent.transform.rotation.z);
        Detector = gameObject.GetComponentInParent<TurretDetector>();
        ShootCooldown = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates the angle to rotate to
        Vector3 diff = Player.transform.position - BarrelPivot.position;

        diff.Normalize();

        float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // when the player is seen by the turret
        if (InSights)
        {
            Debug.Log(rot_Z);   
            BarrelPivot.rotation = Quaternion.Euler(0f, 0f, rot_Z - 90);
            gameObject.GetComponentInParent<Animator>().SetBool("Alerted", true);
            Debug.DrawLine(transform.position, Player.transform.position);
        }
        else
        {
            gameObject.GetComponentInParent<Animator>().SetBool("Alerted", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Detector.WithinRange)
            {
                InSights = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Detector.WithinRange == false)
            {
                InSights = false;
                ShootCooldown = FireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Detector.WithinRange == false || (Detector.WithinRange && InSights == false))
        {

            if (Increasing)
            {
                BarrelPivot.Rotate(0,0,1);
            }
            else
            {
                BarrelPivot.Rotate(0, 0, -1);
            }
        }

        if (ShootCooldown > 0 && Detector.WithinRange && InSights)
        {
            ShootCooldown -= Time.smoothDeltaTime;
        }
        else if(ShootCooldown <= 0)
        {
            ShootCooldown = FireRate;
            gameObject.GetComponentInParent<Animator>().SetTrigger("Shoot");
            Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 diff = Player.transform.position - BarrelPivot.position;
        diff.Normalize();

        ProjectileClone = Instantiate(Projectile, LaunchZone.position, Quaternion.identity);
        Destroy(ProjectileClone, 3f);
        ProjectileClone.GetComponent<Rigidbody2D>().velocity = diff * ProjectileSpeed;
    }
}
