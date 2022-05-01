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
    public AnimatorOverrideController RainbowBoyOverride;
    public AnimatorOverrideController BunnyOverride;
    public AnimatorOverrideController InvertedOverride;
    public AnimatorOverrideController CyclopsOverride;
    public AnimatorOverrideController BonesOverride;
    public AnimatorOverrideController GrampsOverride;
    public AnimatorOverrideController FrostOverride;
    public AnimatorOverrideController CavityOverride;










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
        if (PlayerData.CostumeIndex == 0) // Ninja Frog
        {
            CurrentAnimator.runtimeAnimatorController = NinjaFrogOverride;
        }
        else if (PlayerData.CostumeIndex == 1) // Virtual guy
        {
            CurrentAnimator.runtimeAnimatorController = VirtualGuyOverride;
        }
        else if (PlayerData.CostumeIndex == 2) // Mask Dude
        {
            CurrentAnimator.runtimeAnimatorController = MaskDudeOverride;
        }
        else if (PlayerData.CostumeIndex == 3) // Pink Guy
        {
            CurrentAnimator.runtimeAnimatorController = PinkManOverride;
        }
        else if (PlayerData.CostumeIndex == 4) // Golden Frog
        {
            CurrentAnimator.runtimeAnimatorController = GoldenFrogOverride;
        }
        else if (PlayerData.CostumeIndex == 5) // Rainbow Boy
        {
            CurrentAnimator.runtimeAnimatorController = RainbowBoyOverride;
        }
        else if (PlayerData.CostumeIndex == 6) // Hopps
        {
            CurrentAnimator.runtimeAnimatorController = BunnyOverride;
        }
        else if (PlayerData.CostumeIndex == 7) // g o r F
        {
            CurrentAnimator.runtimeAnimatorController = InvertedOverride;
        }
        else if (PlayerData.CostumeIndex == 8) // Igorrr
        {
            CurrentAnimator.runtimeAnimatorController = CyclopsOverride;
        }
        else if (PlayerData.CostumeIndex == 9) // Famine
        {
            CurrentAnimator.runtimeAnimatorController = BonesOverride;
        }
        else if (PlayerData.CostumeIndex == 10) // Gramps
        {
            CurrentAnimator.runtimeAnimatorController = GrampsOverride;
        }
        else if (PlayerData.CostumeIndex == 11) // Frost
        {
            CurrentAnimator.runtimeAnimatorController = FrostOverride;
        }
        else if (PlayerData.CostumeIndex == 12) // Cavity
        {
            CurrentAnimator.runtimeAnimatorController = CavityOverride;
        }
    }
}
