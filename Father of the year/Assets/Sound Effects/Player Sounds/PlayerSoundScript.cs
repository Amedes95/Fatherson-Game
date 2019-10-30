using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    public AudioSource JumpSource;
    public AudioClip JumpClip;
    public AudioSource WallJumpSource;
    public AudioClip WallJumpClip;

    private void Awake()
    {
        JumpSource.clip = JumpClip;
        WallJumpSource.clip = WallJumpClip;
    }

    public void playJumpSound()
    {
        JumpSource.Play();
    }

    public void playWallJumpSound()
    {
        WallJumpSource.Play();
    }
}
