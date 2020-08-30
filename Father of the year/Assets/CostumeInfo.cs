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
            if (PlayerPrefs.GetInt("Ancient Evil") == 1)
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
            if (PlayerPrefs.GetInt("Flea Flee") == 1)
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
            if (PlayerPrefs.GetInt("Fungus Among Us") == 1)
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
            if (PlayerPrefs.GetInt("Ghastly Escape") == 1)
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
            if (PlayerPrefs.GetInt("Frostbitten") == 1)
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
            if (PlayerPrefs.GetInt("Party Crasher") == 1)
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
            if (PlayerPrefs.GetInt("Fast Food") == 1)
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
            if (PlayerPrefs.GetInt("Lucky 200") == 1)
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
            if (PlayerPrefs.GetInt("Indigestible") == 1)
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
            if (PlayerPrefs.GetInt("Insatiable Appetite") == 1)
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
            if (PlayerPrefs.GetInt("Fossilized") == 1)
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
            if (PlayerPrefs.GetInt("Spoiled Appetite") == 1)
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
