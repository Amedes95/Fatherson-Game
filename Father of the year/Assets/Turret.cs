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


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //BarrelPivot.rotation = Quaternion.Euler(0f, 0f, transform.parent.transform.rotation.z);
        Detector = gameObject.GetComponentInParent<TurretDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = Player.transform.position - BarrelPivot.position;

        diff.Normalize();

        float rot_Z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (InSights)
        {
            BarrelPivot.rotation = Quaternion.Euler(0f, 0f, rot_Z - 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Detector.WithinRange)
            {
                //gameObject.GetComponentInParent<Animator>().enabled = false;
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

    }
}
