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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rotationVector);

    }

    void CalculateFanStrength()
    {
        playerDistance = playerPosition.y - fanPosition.y;
        fanStrength = 1 / (1 + playerDistance) * fanConstant;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerPosition = collision.transform.position;
            CalculateFanStrength();
            collision.attachedRigidbody.AddForce(rotationVector * (fanStrength * collision.attachedRigidbody.mass));
        }
    }
}

