using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkeletonAudio : MonoBehaviour
{

    AudioSource Skeleton;
    public AudioClip FreezeUp;
    public AudioClip Hurt;
    public AudioClip Laugh;

    // Start is called before the first frame update
    void Start()
    {
        Skeleton = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFreezeSFX()
    {
        Skeleton.clip = FreezeUp;
        Skeleton.Play();
    }

    public void PlayHurtSFX()
    {
        Skeleton.clip = Hurt;
        Skeleton.Play();
    }

    public void LaughSFX()
    {
        Skeleton.clip = Laugh;
        Skeleton.Play();
    }

}
