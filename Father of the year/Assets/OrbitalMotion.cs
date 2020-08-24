using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    float timeCounter = 0f;
    Transform center; // copy and paste the starting position from parent transform
    public float radius;
    public float speed;
    public bool clockwise;
    float x;
    float y;
    bool expanding;
    bool shrinking;
    int start;

    public float SpinRate;

    // Start is called before the first frame update
    void Start()
    {
        expanding = true;
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
        if (expanding)
        {
            timeCounter += Time.smoothDeltaTime;
        }
        else if (shrinking)
        {
            timeCounter -= Time.smoothDeltaTime;
        }

        if (timeCounter <= 0)
        {
            shrinking = false;
            expanding = true;
        }
        else if (timeCounter >= 29)
        {
            shrinking = true;
            expanding = false;
        }

        radius = timeCounter % 30; //can be used to make saws expand outwards
        x = radius * Mathf.Cos(Mathf.PI * (timeCounter));
        y = radius * Mathf.Sin(Mathf.PI * (timeCounter));


        transform.position = center.position + new Vector3(x, y, 0);
        transform.parent.Rotate(0, 0, SpinRate);
    }
}
