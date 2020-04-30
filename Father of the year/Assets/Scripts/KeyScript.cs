using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyScript : MonoBehaviour
{

    public float size; //5 is a good size
    public GameObject KeyParticles;
    public static GameObject ParticlesCopy;

    // Start is called before the first frame update
    void Start()
    {
        size = size / transform.parent.transform.localScale.x;
        transform.localScale = new Vector3(size, size, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject.transform.parent.gameObject); // Using destroy instead because of fan logic
            //this.transform.parent.gameObject.SetActive(false);
            ParticlesCopy = Instantiate(KeyParticles, transform.position, Quaternion.identity);
            Destroy(ParticlesCopy, 3f); // destroys particles after 3 seconds
        }
    }

}
