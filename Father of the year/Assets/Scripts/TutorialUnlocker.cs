using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUnlocker : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Tutorial_Complete", 1); // 0 for no, 1 for yes
        }
    }

}
