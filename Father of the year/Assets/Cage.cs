using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public BoxCollider2D BoxCollider;
    public BoxCollider2D Trigger;
    public string WorldHubExitLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Feet")
        {
            collision.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * PlayerMovement.jumpForce * 250);
            gameObject.GetComponent<Animator>().SetTrigger("Break");
            BoxCollider.enabled = false;
            Trigger.enabled = false;
            PlayerPrefs.SetString("ExitedLevel", WorldHubExitLevel);
        }
    }

    private void Start()
    {
        PlayerPrefs.SetString("ExitedLevel", WorldHubExitLevel);
    }
}
