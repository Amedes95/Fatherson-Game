using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int Value; // How nutritious is this fruit?

    private void OnTriggerEnter2D(Collider2D collision) // Triggered when fruit is picked up by the player
    {
        if (collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("Collected"); // pop
            collision.GetComponent<Collector>().FruitFromLevel += Value; // The players current fruit plus the value of the fruit obtained
        }
    }
}
