using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        RisingLava.Rising = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLavaRise()
    {
        RisingLava.Rising = true;
    }

    public void PauseLavaRise()
    {
        RisingLava.Rising = false;
    }

}
