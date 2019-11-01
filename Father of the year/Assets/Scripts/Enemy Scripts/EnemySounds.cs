using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource SoundSource;
    public AudioClip DeathSound;

   public void playDeathSound()
    {
        SoundSource.clip = DeathSound;
        SoundSource.Play();
    }
}
