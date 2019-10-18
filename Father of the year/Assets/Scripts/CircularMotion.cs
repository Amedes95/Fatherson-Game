using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public float timeCounter;
    public Vector3 initialPosition; // copy and paste the starting position from parent transform
    public float radius;
    public float speed;
    public bool clockwise;
    int direction;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        if (clockwise)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.smoothDeltaTime;
        x = radius * Mathf.Cos(timeCounter * speed * 2 * Mathf.PI * direction);
        y = radius * Mathf.Sin(timeCounter * speed * 2 * Mathf.PI * direction);

        transform.position =  initialPosition + new Vector3(x, y, 0);
    }
}
