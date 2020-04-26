using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeper : MonoBehaviour
{
    public bool awake;
    Transform Player;
    public bool SightBlocked;
    public GameObject BonkableHead;
    public GameObject DisturbZone;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();
        if (awake)
        {
            GetComponent<BasicPatrol>().enabled = true;
            if (PlayerMovement.PlayerInvincible)
            {
                BonkableHead.SetActive(true);
            }
            else
            {
                BonkableHead.SetActive(false);
            }
        }
    }

    public void AwakenMe()
    {
        awake = true;
        BonkableHead.SetActive(false);
        DisturbZone.SetActive(false);

    }

    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(transform.position, Player.position, Color.green);
        SightBlocked = Physics2D.Linecast(transform.position, Player.position, 1 << LayerMask.NameToLayer("Ground"));
    }

}
