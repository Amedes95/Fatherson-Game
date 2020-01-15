using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyScript : MonoBehaviour
{

public float size; //5 is a good size

    // Start is called before the first frame update
    void Start()
    {
        size = size / transform.parent.transform.localScale.x;
        transform.localScale = new Vector3(size, size, 1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
