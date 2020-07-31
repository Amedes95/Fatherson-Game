using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBall : MonoBehaviour
{
    public GameObject Disco;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyDiscoBall()
    {
        Disco.GetComponent<SpriteRenderer>().enabled = false;
    }
}
