using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{

    public int WallHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WallHP < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossLaser")
        {
            //Destroy(gameObject);
            Destroy(collision.gameObject);
            WallHP -= 1;
        }
        else if (collision.tag == "Stalactite") // stalactites do 1 damage each
        {
            Destroy(collision.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
            WallHP -= 1;
        }
    }
}
