using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    public GameObject DeathParticles;
    public static GameObject DeathPartcilesClone;
    public GameObject BossDrop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet" && JumpDetector.OnGround == false)
        {
            DeathPartcilesClone = Instantiate(DeathParticles, new Vector2(transform.position.x, transform.position.y -1.5f), Quaternion.identity);
            Destroy(DeathPartcilesClone, 3f);

            // drops the key when killed
            BossDrop.transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f);
            BossDrop.SetActive(true);

            collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 150);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
