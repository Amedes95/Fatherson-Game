using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim;

    void Start()
    {
        playerAnim = gameObject.GetComponent<Animator>();   
    }


    void Update()
    {
        
    }
}
