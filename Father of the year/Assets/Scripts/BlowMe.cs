using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowMe : MonoBehaviour
{

    Vector2 fanPosition;
    Vector2 playerPosition;
    float playerDistance;
    public float fanConstant;
    float fanStrength;

    // Start is called before the first frame update
    void Start()
    {
        fanPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void CalculateFanStrength()
    {
        playerDistance = playerPosition.y - fanPosition.y;
        fanStrength = 1 / (1 + playerDistance) * fanConstant;
        Debug.Log((1 + playerDistance));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPosition = collision.transform.position;
            CalculateFanStrength();
            collision.attachedRigidbody.AddForce(Vector2.up * (fanStrength * collision.attachedRigidbody.mass));
        }
    }
}

