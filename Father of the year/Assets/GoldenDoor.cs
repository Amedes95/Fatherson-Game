using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldenDoor : MonoBehaviour
{
    public float size; //5 is a good size
    public GameObject KeyParticles;
    public static GameObject ParticlesCopy;
    public TextMeshPro CoinCount;

    public float CointNeeded;
    public static float CoinsToCollect;

    // Start is called before the first frame update
    void Start()
    {
        size = size / transform.parent.transform.localScale.x;
        transform.localScale = new Vector3(size, size, 1);
        CoinsToCollect = CointNeeded;
    }

    private void Update()
    {
        CoinCount.text = CoinsToCollect.ToString();
        if (CoinsToCollect <= 0) // all coins required to open door are collected
        {
            UnlockDoor(); // poof, ya did it
        }
    }


    public void UnlockDoor() // unlocks door by teleporting key directly to player
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) // when the player picks up they key
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject.transform.parent.gameObject); // Using destroy instead because of fan logic
            ParticlesCopy = Instantiate(KeyParticles, transform.position, Quaternion.identity);
            Destroy(ParticlesCopy, 3f); // destroys particles after 3 seconds
        }
    }
}
