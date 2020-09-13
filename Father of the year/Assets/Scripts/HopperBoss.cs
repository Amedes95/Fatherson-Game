using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBoss : MonoBehaviour
{
    bool Awakened;
    int currentPhase;
    float idleDuration;
    Animator HopperAnimator;
    bool TouchingWall;
    bool TouchingFloor;
    public Transform Endline;
    public Transform FloorLine;
    Rigidbody2D HopperBody;
    public Vector2 HopDirection = new Vector2(1, 0);
    public float HopForce;
    public stalactite[] Stalactites;
    public stalactite[] Stalactites2;
    public stalactite[] Stalactites3;

    public GameObject SpitPrefab;
    public static GameObject SpitPrefabClone;
    public Transform SpitSpawn;
    public float SpitSpeed;

    public GameObject Zzz;




    // Start is called before the first frame update
    void Start()
    {
        HopperAnimator = gameObject.GetComponent<Animator>();
        HopperBody = gameObject.GetComponent<Rigidbody2D>();
        LockMovement();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastingWall();
        RaycastingFloor();
        if (TouchingWall)
        {
            FlipCharacter();
            HopDirection = new Vector2(HopDirection.x * -1, 0);
            if (currentPhase == 0 || currentPhase == 2)
            {
                HopperAnimator.SetTrigger("Spit");
            }
        }
        if (TouchingFloor)
        {
            HopperAnimator.SetBool("Grounded", true);
        }
        else
        {
            HopperAnimator.SetBool("Grounded", false);
        }
        if (gameObject.GetComponentInChildren<BonkableHead>().CurrentHP < gameObject.GetComponentInChildren<BonkableHead>().MaxHP)
        {
            Zzz.SetActive(false);
        }

    }

    public void Spit()
    {
        SpitPrefabClone = Instantiate(SpitPrefab, SpitSpawn.position, gameObject.transform.rotation);
        SpitPrefabClone.GetComponent<Rigidbody2D>().velocity = new Vector2(HopDirection.x * SpitSpeed, 0);
    }

    public void HopForward()
    {
        HopperBody.AddForce(HopDirection * HopForce);
    }

    public void Roar()
    {
        currentPhase += 1;
        gameObject.GetComponent<HopperSFX>().PlayRoarSound();
        if (currentPhase == 1) // round 1
        {
            foreach (stalactite Stalactite in Stalactites)
            {
                Stalactite.Fall();
            }
        }
        if (currentPhase == 2) // round 2
        {
            foreach (stalactite Stalactite in Stalactites2)
            {
                Stalactite.Fall();
            }
        }
        if (currentPhase == 3) // final round
        {
            foreach (stalactite Stalactite in Stalactites3)
            {
                Stalactite.Fall();
            }
        }
        Debug.Log(currentPhase);

    }

    void RaycastingWall()
    {
        Debug.DrawLine(transform.position, Endline.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingWall = Physics2D.Linecast(transform.position, Endline.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile

    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, FloorLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingFloor = Physics2D.Linecast(transform.position, FloorLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile
    }

    public void FlipCharacter()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    public void UnlockMovement()
    {
        HopperBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void LockMovement()
    {
        HopperBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ResumeHopping()
    {
        HopperAnimator.SetBool("Hopping", true);
    }
}
