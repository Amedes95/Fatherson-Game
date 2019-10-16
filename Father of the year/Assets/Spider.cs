using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    Animator SpiderAnim;
    // Start is called before the first frame update
    void Awake()
    {
        SpiderAnim = gameObject.GetComponent<Animator>();
    }


    public void DecideLength()
    {
        int Distance = Random.Range(1, 4); // Get a random distance (Returns 1,2, or 3)

        if (Distance == 1) // Short
        {
            SpiderAnim.SetTrigger("Short");
        }
        else if (Distance == 2)
        {
            SpiderAnim.SetTrigger("Medium");
        }
        else if (Distance == 3)
        {
            SpiderAnim.SetTrigger("Long");
        }
    }
}
