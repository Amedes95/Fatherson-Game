using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCanvas : MonoBehaviour
{

    public GameObject MenuManager;
    public bool Credits;

    public void SkipIntro()
    {
        if (!Credits)
        {
            MenuManager.GetComponent<MainMenu>().SkipIntro();
        }
        else
        {
            MenuManager.GetComponent<CreditsManager>().SkipOutro();
        }
    }
}
