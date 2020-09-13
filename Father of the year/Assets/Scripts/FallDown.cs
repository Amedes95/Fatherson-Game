using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{

    public Transform PlayerDetectorEnd;
    bool PlayerTouched;
    GameObject Player;
    bool SightsBlocked;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastPlayer();
        RaycastSights();
        if (PlayerTouched && !SightsBlocked)
        {
            // unlocks the gravity
            gameObject.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            gameObject.transform.parent.rotation = new Quaternion(0, transform.parent.rotation.y, transform.parent.rotation.z, 1); // flips the enemy
            PlayerDetectorEnd.gameObject.SetActive(false); // get rid of fall detector
            if (gameObject.GetComponentInParent<BasicPatrol>().TouchingFloor)
            {
                gameObject.GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

    }

    public void RaycastPlayer()
    {
        Debug.DrawLine(transform.position, PlayerDetectorEnd.position, Color.green);
        PlayerTouched = Physics2D.Linecast(transform.position, PlayerDetectorEnd.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void RaycastSights()
    {
        Debug.DrawLine(transform.position, Player.transform.position, Color.green);
        SightsBlocked = Physics2D.Linecast(transform.position, Player.transform.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
