using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAdvice : MonoBehaviour
{
    public GameObject SpeechBubble;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SpeechBubble.GetComponent<Animator>().SetTrigger("Help");
        }
    }
}
