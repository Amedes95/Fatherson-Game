using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{

    Vector2 fanPosition;
    Vector2 playerPosition;
    float playerDistance;
    public float fanConstant;
    float fanStrength;
    public float rotation;
    Vector2 rotationVector;

    // Start is called before the first frame update
    void Start()
    {
        fanPosition = transform.position;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        rotationVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (rotation + 90)), Mathf.Sin(Mathf.Deg2Rad * (rotation + 90)));
        //Debug.Log(rotationVector);
    }

    void CalculateFanStrength() // only for upright fans right now
    {
        if (rotationVector.y == 1) // vertical
        {
            playerDistance = playerPosition.y - fanPosition.y;
        }
        else if (rotationVector.y == 0) // horizontal
        {
            playerDistance = playerPosition.x - fanPosition.x;
        }
        fanStrength = 1 / (1 + playerDistance) * fanConstant;


        if (fanStrength > 30) // sometimes you just need to stop
        {
            fanStrength = 30;
        }
        //Debug.Log(fanStrength);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().FanFloating = true;
            playerPosition = collision.transform.position;
            CalculateFanStrength();
            if (collision.GetComponent<Rigidbody2D>().velocity.y > 10) // This is new 3/1/20
            {
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, 10);
            }
            collision.attachedRigidbody.AddForce(rotationVector * (fanStrength * collision.attachedRigidbody.mass));
            //Debug.Log(collision.GetComponent<Rigidbody2D>().velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().FanFloating = false;
        }
    }
}

