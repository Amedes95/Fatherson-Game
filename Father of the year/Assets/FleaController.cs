using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaController : MonoBehaviour
{
    public bool FightTriggered;
    TriggerBoss BossTrigger;
    Vector2 WalkDirection;
    float FaceDirection;
    public bool Walking;
    public bool Shooting;
    public float WalkSpeed;

    public GameObject LaserSpawnPos;
    Vector2 LaserTargetPos;
    public GameObject LaserPrefab;
    public static GameObject LaserPrefabClone;
    GameObject Player;
    public Transform RayCastEnd;
    bool TouchingFloor;
    public BoxCollider2D LaserTriggerZone;




    // Start is called before the first frame update
    void Start()
    {
        Walking = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        BossTrigger = gameObject.GetComponentInChildren<TriggerBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastingFloor();
        LaserTargetPos = Player.transform.position;
        if (Player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            FaceDirection = -1;
        }
        else if (Player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            FaceDirection = 1;
        }


        if (!TouchingFloor && Walking == false)
        {
            Walking = true;
        }

        if (TouchingFloor && Walking && FightTriggered)
        {
            Walk();
        }

        if (TouchingFloor == false)
        {
            LaserTriggerZone.enabled = false;
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = true;
            LaserTriggerZone.enabled = true;
        }
    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, RayCastEnd.position, Color.green);
        TouchingFloor = Physics2D.Linecast(transform.position, RayCastEnd.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    public void DisableAnimator()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void Walk()
    {
        WalkDirection = new Vector2(FaceDirection, 0) * WalkSpeed;
        gameObject.GetComponent<Animator>().SetBool("Walking", true);
        gameObject.GetComponent<Rigidbody2D>().velocity = WalkDirection;
    }

    public void Hop(float jumpForce)
    {
        Debug.Log("Hop");
        if (gameObject.GetComponent<Animator>().enabled == true)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Hop");
        }
        Vector2 JumpAngle = new Vector2(FaceDirection / 2, Mathf.Sqrt(3) / 2);
        gameObject.GetComponent<Rigidbody2D>().AddForce(JumpAngle * jumpForce * 5);
        Walking = true;
    }

    public void FireLaser()
    {
        float DirectionX = (Player.transform.position.x - LaserSpawnPos.transform.position.x);
        float DirectionY = ((Player.transform.position.y) - LaserSpawnPos.transform.position.y);
        Vector3 movement = new Vector3(DirectionX, DirectionY);

        // Spawn Laser Object and move it
        LaserPrefabClone = Instantiate(LaserPrefab, LaserSpawnPos.transform.position, Quaternion.identity);
        LaserPrefabClone.GetComponent<Rigidbody2D>().AddForce(movement);
        LaserPrefabClone.GetComponent<Rigidbody2D>().velocity = movement;
        Destroy(LaserPrefabClone, 3f);
        //Debug.Log("Firin ma laza");
    }
}
