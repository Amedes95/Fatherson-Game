using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    Animator SpiderAnim;
    Vector2 JumpAngle;
    public float JumpSpeed;
    float Direction;
    public Transform EndLine;
    bool TouchingFloor;


    // Start is called before the first frame update
    void Awake()
    {
        SpiderAnim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Direction = Mathf.Sign(transform.localScale.x);
        RaycastingFloor();
        if (TouchingFloor == false)
        {
            SpiderAnim.SetBool("InAir", true);
        }
        else
        {
            SpiderAnim.SetBool("InAir", false);
        }
    }

    public void DecideLength()
    {
        int Distance = Random.Range(1, 4); // Get a random distance (Returns 1,2, or 3)

        if (Distance == 1) // Short
        {
            SpiderAnim.SetTrigger("Short");
        }
        else if (Distance == 2)
        {
            SpiderAnim.SetTrigger("Medium");
        }
        else if (Distance == 3)
        {
            SpiderAnim.SetTrigger("Long");
        }
    }

    public void Hop() // called during the last frame of the idle animation
    {
        if (TouchingFloor == true)
        {
            Debug.Log("Hippty Hoppity");
            JumpAngle = new Vector2(Direction / 2, Mathf.Sqrt(3) / 2);
            gameObject.GetComponent<Rigidbody2D>().AddForce(JumpAngle * JumpSpeed);
            SpiderAnim.SetTrigger("Hop");
        }

    }

    void RaycastingFloor()
    {
        Debug.DrawLine(transform.position, EndLine.position, Color.green);  // during playtime, projects a line from a start point to and end point
        TouchingFloor = Physics2D.Linecast(transform.position, EndLine.position, 1 << LayerMask.NameToLayer("Ground")); // returns true if line touches a ground tile
    }
}
