using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float Speed;
    public bool MovingRight;

    private void Update()
    {
        if (MovingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0);

        }
        if (!MovingRight)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-Speed, 0);
        }
    }
}
