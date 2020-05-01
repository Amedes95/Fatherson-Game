using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float Speed;
    bool InAir;
    public int Jumps;
    public GameObject GhostBoss;
    public GameObject Flamepillar1;
    public GameObject Flamepillar2;
    // Start is called before the first frame update

    private void Awake()
    {
        Jumps = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (InAir == false)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Speed;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Speed);

        }
    }

    public void Hop(float jumpForce)
    {
        InAir = true;
        if (gameObject.GetComponent<Animator>().enabled == true)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }
        Vector2 JumpAngle = new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2));
        gameObject.GetComponent<Rigidbody2D>().AddForce(JumpAngle * jumpForce * 5);
    }

    public void Jumping()
    {
        InAir = true;
    }
    public void Landing()
    {
        InAir = false;
    }

    public void Attack()
    {
        Jumps -= 1;
        if (Jumps <= 0)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    public void BanishGhost()
    {
        GhostBoss.GetComponent<Animator>().SetTrigger("Killed");
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        Flamepillar1.SetActive(false);
        Flamepillar2.SetActive(false);
    }

}
