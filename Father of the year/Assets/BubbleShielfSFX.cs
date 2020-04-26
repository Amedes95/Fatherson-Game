using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShielfSFX : MonoBehaviour
{
    public void PlayWarningSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
