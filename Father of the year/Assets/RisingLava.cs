using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{

    public static float RiseSpeed;
    public static bool Rising;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Rising && PlayerHealth.Dead == false)
        {
            Rise();
        }
    }

    public void Rise()
    {
        transform.parent.Translate(Vector2.up * RiseSpeed);
    }
}
