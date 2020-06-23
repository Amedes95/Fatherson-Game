using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSnowball : MonoBehaviour
{
    public GameObject SnowParticles;
    public static GameObject SnowParticlesClone;
    public bool DestroyOnImpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x) < 1.8f)
        {
            SnowParticlesClone = Instantiate(SnowParticles, transform.position, Quaternion.identity);
            Destroy(SnowParticlesClone, 4f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DestroyOnImpact && (collision.tag == "Player" || collision.tag == "Ground"))
        {
            SnowParticlesClone = Instantiate(SnowParticles, transform.position, Quaternion.identity);
            Destroy(SnowParticlesClone, 4f);
            Destroy(gameObject);
        }
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            SnowParticlesClone = Instantiate(SnowParticles, transform.position, Quaternion.identity);
            Destroy(SnowParticlesClone, 4f);
            Destroy(gameObject);
        }
    }
}
