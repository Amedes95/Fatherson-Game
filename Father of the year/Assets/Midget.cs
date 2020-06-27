using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Midget : MonoBehaviour
{
    public float Speed;
    Vector2 Velocity;
    float Ypos;
    bool Grounded;
    public GameObject EndPortal;
    public Transform PortalPos;
    Vector3 PortalMovePos;
    // Start is called before the first frame update


    private void Awake()
    {
        PortalMovePos = PortalPos.position;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Velocity;
        if (Grounded)
        {
            if (transform.position.y < Ypos)
            {
                Velocity = Vector2.right * Speed / 2;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 50;
            }
        }

    }

    public void MoveRight()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x == 0)
        {
            Velocity = Vector2.right * Speed;
        }
    }
    public void CheckHeight()
    {
        Grounded = true;
        Ypos = gameObject.transform.position.y;
    }

    private void OnDisable()
    {
        EndPortal.transform.position = PortalMovePos;
    }
}
