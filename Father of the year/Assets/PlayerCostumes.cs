using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostumes : MonoBehaviour
{
    public AnimatorOverrideController NinjaFrogOverride;
    public AnimatorOverrideController VirtualGuyOverride;
    public AnimatorOverrideController MaskDudeOverride;
    public AnimatorOverrideController PinkManOverride;
    public AnimatorOverrideController GoldenFrogOverride;



    Animator CurrentAnimator;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /// sets the player costume when loading into levels
        if (PlayerPrefs.GetInt("CostumeIndex") == 0) // Ninja Frog
        {
            CurrentAnimator.runtimeAnimatorController = NinjaFrogOverride;
        }
        else if (PlayerPrefs.GetInt("CostumeIndex") == 1) // Virtual guy
        {
            CurrentAnimator.runtimeAnimatorController = VirtualGuyOverride;
        }
        else if (PlayerPrefs.GetInt("CostumeIndex") == 2) // Mask Dude
        {
            CurrentAnimator.runtimeAnimatorController = MaskDudeOverride;
        }
        else if (PlayerPrefs.GetInt("CostumeIndex") == 3) // Pink Guy
        {
            CurrentAnimator.runtimeAnimatorController = PinkManOverride;
        }
        else if (PlayerPrefs.GetInt("CostumeIndex") == 4) // Golden Frog
        {
            CurrentAnimator.runtimeAnimatorController = GoldenFrogOverride;
        }
    }
}
