using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBoos : MonoBehaviour
{
    public int CurrentBossHP;
    public int MAXHP;
    GameObject Player;
    public bool Walking;
    public bool FightTriggered;
    Vector2 WalkDirection;
    float FaceDirection;
    public float WalkSpeed;

    public Transform RayCastEnd;
    bool TouchingFloor;
    float StunDuration;

    bool CanBeKilled;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentBossHP = MAXHP;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.activeInHierarchy == false)
        {
            //gameObject.GetComponent<Animator>().SetBool("PlayerKilled", true);
        }

        RaycastingFloor();
        if ((JumpDetector.OnGround || TouchingFloor == false) && gameObject.GetComponent<Animator>().GetBool("Stunned")== false)
        {
            if (Player.transform.position.x < transform.position.x) // if the player is to the left of the boss
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                FaceDirection = -1;
            }
            else if (Player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                FaceDirection = 1;
            }
        }



        if (TouchingFloor && FightTriggered && Player.activeInHierarchy)
        {
            Walk();
        }
        if (!TouchingFloor && FightTriggered)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            gameObject.GetComponent<Animator>().SetBool("Walking", false);
        }
        if (Player.activeInHierarchy == false)
        {
            gameObject.GetComponent<Animator>().SetBool("Walking", false);
        }


        //// HP Stuff
        if (CurrentBossHP <= 0)
        {
            // kill boss
        }
        else
        {
            if (StunDuration > 0)
            {
                StunDuration -= Time.smoothDeltaTime;
            }
            else
            {
                StunDuration = 0f;
                gameObject.GetComponent<Animator>().SetBool("Stunned", false);
            }
        }


    }

    public void Walk()
    {
        if (Player.activeInHierarchy)
        {
            WalkDirection = new Vector2(FaceDirection, 0) * WalkSpeed;
            gameObject.GetComponent<Animator>().SetBool("Walking", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = WalkDirection;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Walking", false);
        }

    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, RayCastEnd.position, Color.green);
        TouchingFloor = Physics2D.Linecast(transform.position, RayCastEnd.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void LockMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnlockMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void SubtractHP()
    {
        CurrentBossHP -= 1;
        if (CurrentBossHP <= MAXHP - 6)
        {
            StunDuration = 5f;
            StunnedMode();
            CurrentBossHP = MAXHP;
        }
    }

    public void StunnedMode()
    {
        if (StunDuration > 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Stunned", true);
            LockMovement();
        }
        else
        {
            UnlockMovement();
            gameObject.GetComponent<Animator>().SetBool("Stunned", false);
        }
    }

    public void BecomeVulnerable()
    {
        CanBeKilled = true;
    }

    public void BecomeInvulnerable()
    {
        CanBeKilled = false;
    }
}
