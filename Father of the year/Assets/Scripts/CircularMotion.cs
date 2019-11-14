using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    //float timeCounter;
    Transform center; // copy and paste the starting position from parent transform
    //public float radius;
    //public float speed;
    //public float start;
    public bool clockwise;
    //int direction;
    //float x;
    //float y;

    public float SpinRate;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.parent;
        if (clockwise)
        {
            SpinRate = SpinRate * -1f;
        }
        else
        {
            SpinRate = SpinRate * 1f;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //timeCounter += Time.smoothDeltaTime;
        ////radius = timeCounter % 5; //can be used to make saws expand outwards
        //x = radius * Mathf.Cos(2 * Mathf.PI * (timeCounter * speed * direction + start));
        //y = radius * Mathf.Sin(2 * Mathf.PI * (timeCounter * speed * direction + start));

        //transform.position =  center.position + new Vector3(x, y, 0);
        transform.parent.Rotate(0, 0, SpinRate);
    }
}
