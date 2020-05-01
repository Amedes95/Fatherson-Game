using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGhost : MonoBehaviour
{

    public GameObject GhostBoss;
    public Transform BossMovePos;
    public bool FaceBossRight;
    public bool PauseLavaToo;
    public float NewLavaSpeed;
    public bool SpawnLavaPillars;
    public GameObject LavaPillar1;
    public GameObject LavaPillar2;
    public bool RemoveKnigt;
    public GameObject Knight;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GhostBoss.GetComponent<Animator>().SetTrigger("Appear");
            GhostBoss.transform.position = BossMovePos.position;
            if (FaceBossRight)
            {
                GhostBoss.transform.rotation = new Quaternion(0, -180, 0, 0);
            }
            else
            {
                GhostBoss.transform.rotation = new Quaternion(0, -0, 0, 0);
            }
            if (PauseLavaToo)
            {
                RisingLava.Rising = false;
                RisingLava.RiseSpeed = NewLavaSpeed;
            }
            if (SpawnLavaPillars)
            {
                LavaPillar1.SetActive(true);
                LavaPillar2.SetActive(true);
            }
            if (RemoveKnigt)
            {
                Knight.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}
