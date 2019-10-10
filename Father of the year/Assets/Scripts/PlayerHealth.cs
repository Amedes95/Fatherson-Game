using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static bool Dead; // it's static because I refence it directly from the playermovement && and death canvas script

    void Awake() // we don't start dead when the scene loads now do we?
    {
        Dead = false;
    }

    public void KillPlayer() // Kills player
    {
        Dead = true; // oof
    }
}
