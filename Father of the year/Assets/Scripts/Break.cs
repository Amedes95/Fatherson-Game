using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{

    public int WallHP;
    public GameObject BreakParticles;
    public static GameObject BreakParticlesClone;

    AudioSource WallSource;

    // Start is called before the first frame update
    void Start()
    {
        WallSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WallHP < 0)
        {
            SpawnBreakParticles();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BossLaser")
        {
            collision.GetComponent<DestroyOnGround>().SpawnImpactParticles();
            gameObject.GetComponent<Animator>().SetTrigger("Damaged");
            Destroy(collision.gameObject);
            WallSource.Play();
            WallHP -= 1;
        }
        else if (collision.tag == "Stalactite") // stalactites do 1 damage each
        {
            WallSource.Play();
            Destroy(collision.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
            gameObject.GetComponent<Animator>().SetTrigger("Damaged");
            WallHP -= 1;
        }
    }

    public void SpawnBreakParticles()
    {
        BreakParticlesClone = Instantiate(BreakParticles, transform.position, transform.rotation);
        Destroy(BreakParticlesClone, 3f);
    }
}
