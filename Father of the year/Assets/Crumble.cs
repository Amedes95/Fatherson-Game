using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet" || collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Crumble");
        }
    }

    public void DisableMe()
    {
        gameObject.SetActive(false);
    }
}
