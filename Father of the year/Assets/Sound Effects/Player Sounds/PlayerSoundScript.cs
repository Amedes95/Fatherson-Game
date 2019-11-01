﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    public AudioSource JumpSource;
    public AudioClip JumpClip;
    public AudioClip WallJumpClip;
    public AudioClip LandingClip;

    private void Awake()
    {
        
    }

    public void playJumpSound()
    {
        JumpSource.clip = JumpClip;
        JumpSource.Play();
    }

    public void playWallJumpSound()
    {
        JumpSource.clip = WallJumpClip;
        JumpSource.Play();
    }

    public void playLandingSound()
    {
        JumpSource.clip = LandingClip;
        JumpSource.Play();
    }
}
