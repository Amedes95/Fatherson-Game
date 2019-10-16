using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour
{
    Sleeper Enemy;

    // Start is called before the first frame update
    void Awake()
    {
        Enemy = GetComponentInParent<Sleeper>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !Enemy.SightBlocked)
        {
            GetComponentInParent<Animator>().SetTrigger("Wake");
        }
    }
}
