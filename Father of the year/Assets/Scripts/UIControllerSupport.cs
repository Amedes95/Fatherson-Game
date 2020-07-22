using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIControllerSupport : MonoBehaviour
{
    public GameObject InitialHighlight;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (Boombox.ControllerModeEnabled)
        {
            if (EventSystem.current.currentSelectedGameObject == InitialHighlight) // prevents the bug where going into audio settings and coming out removes button focus
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            EventSystem.current.SetSelectedGameObject(InitialHighlight);
        }
    }


    private void Update()
    {
        // if you press escape to pull up the mouse, return focus to the buttons when clicking on screen again
        if (EventSystem.current.currentSelectedGameObject == null && Boombox.ControllerModeEnabled) 
        {
            EventSystem.current.SetSelectedGameObject(InitialHighlight);
        }
        else if (EventSystem.current.currentSelectedGameObject != null && Boombox.ControllerModeEnabled == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void FindFocus()
    {
        if (Boombox.ControllerModeEnabled)
        {
            if (EventSystem.current.currentSelectedGameObject == InitialHighlight) // prevents the bug where going into audio settings and coming out removes button focus
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
            EventSystem.current.SetSelectedGameObject(InitialHighlight);
        }
    }





}
