using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIControllerSupport : MonoBehaviour
{
    public GameObject InitialHighlight;
    EventSystem EventSystem;

    public EventSystem MainMenuSystem;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") // not a main menu / hub
        {
            EventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
            EventSystem.SetSelectedGameObject(null);
            EventSystem.SetSelectedGameObject(InitialHighlight);
        }
        else // main menu
        {
            MainMenuSystem.SetSelectedGameObject(null);
            MainMenuSystem.SetSelectedGameObject(InitialHighlight);
        }

    }


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            MainMenuSystem.SetSelectedGameObject(null);
            MainMenuSystem.SetSelectedGameObject(InitialHighlight);
        }
    }
}
