using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MonoBehaviour
{
    public AudioClip LevelMusic;
    BackgroundMusic BGMusic;
    public GameObject BGPrefab;
    public static bool EditorMode;

    public static bool ControllerModeEnabled;

    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        if (GameObject.FindGameObjectWithTag("BGMusic") == null)
        {
            EditorMode = true;
            Instantiate(BGPrefab);
            BGPrefab.GetComponent<BackgroundMusic>().LevelMusic = LevelMusic;
            BGPrefab.GetComponent<BackgroundMusic>().CompareSongs();
        }
        else
        {
            BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.CompareSongs();
        }

    }

    private void Update()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    ControllerModeEnabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Debug.Log("Controller Used:" + temp[i].ToString());

                    // This is a shitty way of identifying what controller you have, but it works
                    if (temp[i].ToString() == "Controller (Xbox One For Windows)")
                    {
                        Debug.Log("That's an xbox controller plugged in");
                    }

                }
                else
                {
                    ControllerModeEnabled = false;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    //If it is empty, controller i is disconnected
                    //where i indicates the controller number
                }
            }
        }
    }
}
