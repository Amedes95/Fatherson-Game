using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LungeBox : MonoBehaviour
{
    Vector2 PatrolDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponentInParent<Animator>().SetTrigger("Attack");
            PatrolDirection = gameObject.GetComponentInParent<BasicPatrol>().PatrolDirection;
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(PatrolDirection * 500);
        }
    }
}
