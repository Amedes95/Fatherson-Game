using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeInfo : MonoBehaviour
{
    public string CostumeName;
    public string SecretName;
    public bool Locked;
    public GameObject LockedSymbol;

    private void Update()
    {
        if (CostumeName == "Ninja Frog") // always unlock ya boi Ninja Frog
        {
            Locked = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            LockedSymbol.SetActive(false);
        }

        if (CostumeName == "Golden Frog") // unlocks golden frog after beating geode world
        {
            if (PlayerData.AchievementRecords.ContainsKey("Ancient Evil")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Pink Man") // unlocks pink man after completing world 1 boss
        {
            if (PlayerData.AchievementRecords.ContainsKey("Flea Flee")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Virtual Guy") // unlocks Virtual guy after completing world 3 boss
        {
            if (PlayerData.AchievementRecords.ContainsKey("Fungus Among Us")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Mask Dude") // unlocks mask dude after completing world 4 boss
        {
            if (PlayerData.AchievementRecords.ContainsKey("Ghastly Escape")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Frost") // unlocks frost after completing world 5 boss
        {
            if (PlayerData.AchievementRecords.ContainsKey("Frostbitten")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }

        else if (CostumeName == "Rainbow Boy") // unlocks Rainbow Boy after completing party world
        {
            if (PlayerData.AchievementRecords.ContainsKey("Party Crasher")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Hopps") // unlocks Hopps after beating Vegan mode under 2 hours
        {
            if (PlayerData.AchievementRecords.ContainsKey("Fast Food")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "g o r F") // unlocks GORF after failing 200 times
        {
            if (PlayerData.AchievementRecords.ContainsKey("Lucky 200")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Igorrr") // unlocks Igorrr after completing boss rush
        {
            if (PlayerData.AchievementRecords.ContainsKey("Indigestible")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Famine") // unlocks bones after completing malnourished mode
        {
            if (PlayerData.AchievementRecords.ContainsKey("Insatiable Appetite")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Gramps") // unlocks gramps after completing retro world
        {
            if (PlayerData.AchievementRecords.ContainsKey("Fossilized")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }
        else if (CostumeName == "Cavity") // unlocks Cavity after beating world 2 boss
        {
            if (PlayerData.AchievementRecords.ContainsKey("Spoiled Appetite")) // achievement is unlocked
            {
                Locked = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                LockedSymbol.SetActive(false);
            }
            else
            {
                Locked = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                LockedSymbol.SetActive(true);
            }
        }

    }

    private void Start()
    {
        Locked = true;
        LockedSymbol.SetActive(true);
    }

}
