using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManSFX : MonoBehaviour
{
    public AudioClip Grunt1;
    public AudioClip Grunt2;
    public AudioClip KnockedDown;
    public AudioClip Wounded;
    AudioSource MushroomMan;

    // Start is called before the first frame update
    void Awake()
    {
        MushroomMan = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHurtSound()
    {
        if (Random.Range(0, 2) == 0) // is it 0?
        {
            MushroomMan.clip = Grunt1;
            MushroomMan.Play();
        }
        else // must be a 1
        {
            MushroomMan.clip = Grunt2;
            MushroomMan.Play();
        }
    }

    public void PlayKNockedNoise()
    {
        MushroomMan.clip = KnockedDown;
        MushroomMan.Play();
    }

    public void PlayWoundedNoise()
    {
        MushroomMan.clip = Wounded;
        MushroomMan.Play();
    }
}
