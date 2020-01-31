using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperSFX : MonoBehaviour
{
    public AudioClip Roar;
    public AudioClip BossOuch1;
    public AudioClip BossOuch2;

    AudioSource Hopper;
    public AudioSource BonkableHead;

    // Start is called before the first frame update
    void Start()
    {
        Hopper = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRoarSound()
    {
        Hopper.clip = Roar;
        Hopper.Play();
    }
    public void PlayHurtSound()
    {
        if (Random.Range(0, 2) == 0) // is it 0?
        {
            BonkableHead.clip = BossOuch1;
            BonkableHead.Play();
        }
        else // must be a 1
        {
            BonkableHead.clip = BossOuch2;
            BonkableHead.Play();
        }
    }


}
