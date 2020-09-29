using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Boombox : MonoBehaviour
{
    public AudioClip LevelMusic;
    public AudioClip PartyMixtape;
    public AudioClip OldTImeyMusic;
    public AudioClip NormalMusic;

    BackgroundMusic BGMusic;
    public GameObject BGPrefab;
    public static bool EditorMode;

    public static bool ControllerModeEnabled;
    public static bool XboxEnabled;
    public static bool PS4Enabled;

    public static float vibrateDuration = 0f;
    public static float LowSpeed;
    public static float HighSpeed;

    bool RumbleEnabled;

    public TextMeshProUGUI CheevoText;
    float VeganTimer;
    public bool KeepNormalMusic;
    string[] temp;
    public Texture2D CursorTexture;
    public Texture2D CursorTexture2;


    // Start is called before the first frame update
    void Awake()
    {
        NormalMusic = LevelMusic;
        if (PlayerPrefs.GetInt("PartyModeON") == 1 && KeepNormalMusic == false)
        {
            LevelMusic = PartyMixtape;
        }
        else if (PlayerPrefs.GetInt("OldTimeyON") == 1 && KeepNormalMusic == false)
        {
            LevelMusic = OldTImeyMusic;
        }
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
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }


    private void Update()
    {
        if (PlayerPrefs.GetInt("PartyModeON") == 1)
        {
            LevelMusic = PartyMixtape;
        }
        else if (PlayerPrefs.GetInt("OldTimeyON") == 1)
        {
            LevelMusic = OldTImeyMusic;
        }
        else if(PlayerPrefs.GetInt("PartyModeON") == 0 && PlayerPrefs.GetInt("OldTimeyON") == 0 && KeepNormalMusic == false)
        {
            LevelMusic = NormalMusic;
        }
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(CursorTexture2, Vector2.zero, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }

        //Get Joystick Names
        temp = Input.GetJoystickNames();

        //Debug.Log("LENGTH:" + temp.Length);
        //Debug.Log("CONTROLER NAME: " + temp.ToString());
        //Check whether array contains anything

        if (temp.Length > 0)
        {
            CheckControllers();
        }
        else
        {
            if (Gamepad.current != null)
            {
                ControllerModeEnabled = true;
                Debug.Log(Gamepad.current.displayName);
            }
            else
            {
                ControllerModeEnabled = false;
            }
        }


        if (ControllerModeEnabled) // controller is physically plugged in
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            //RumbleEnabled = true; // always enable if controller is physically plugged in

            if (RumbleEnabled && PlayerPrefs.GetFloat("RumbleToggled") == 1 && PauseMenu.GameIsPaused == false) // but we don't enable rumnble unless it is toggled on
            {
                if (vibrateDuration > 0 && RumbleEnabled) // something was called to vibrate
                {
                    Gamepad.current.ResumeHaptics();
                    vibrateDuration -= Time.smoothDeltaTime; // count down duration
                    VibrateController(); // vibrate while counting down
                }
                else if (vibrateDuration <= 0)
                {
                    vibrateDuration = 0;
                    if (Gamepad.current != null)
                    {
                        Gamepad.current.PauseHaptics();
                    }
                }
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    private void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("VeganMode") == 1 && SceneManager.GetActiveScene().name != "MainMenu" && PauseMenu.GameIsPaused == false) // if vegan mode is active and not at the menu
        {
            VeganTimer = PlayerPrefs.GetFloat("VeganTimer");
            PlayerPrefs.SetFloat("VeganTimer", VeganTimer += Time.smoothDeltaTime);
            //Debug.Log(PlayerPrefs.GetFloat("VeganTimer").ToString("F2"));
            //if (Input.GetKey(KeyCode.O))
            //{
            //    PlayerPrefs.SetFloat("VeganTimer", 3590);
            //}
            string hours = Mathf.Floor(VeganTimer/60/60).ToString("00");
            string minutes = Mathf.Floor((VeganTimer / 60) % 60).ToString("00");
            string seconds = (VeganTimer % 60).ToString("00");

            PlayerPrefs.SetString("VeganHours", hours);
            PlayerPrefs.SetString("VeganMinutes", minutes);
            PlayerPrefs.SetString("VeganSeconds", seconds);


            print(string.Format("{0}:{1}:{2}", hours, minutes, seconds));
        }
    }

    public void VibrateController() // causes vibration of controller
    {
        Gamepad.current.SetMotorSpeeds(LowSpeed, HighSpeed);
    }

    public static void SetVibrationIntensity(float duration, float lowerSpeed, float higherspeed) // sets the variables accordingly
    {
        vibrateDuration = duration;
        LowSpeed = lowerSpeed;
        HighSpeed = higherspeed;
    }


    public void UnlockCheevo(string CheevoName)
    {
        //BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
        BGMusic.UnlockCheevo(CheevoName);

        //CheevoText.text = "Achievement Unlocked: " + CheevoName;
        //gameObject.GetComponentInChildren<Animator>().SetTrigger("Unlock");

    }

    public void UpdateSound()
    {
        //Debug.Log("Updating Sound");
        if (PlayerPrefs.GetInt("PartyModeON") == 1 && KeepNormalMusic == false)
        {
            LevelMusic = PartyMixtape;
        }
        else if (PlayerPrefs.GetInt("OldTimeyON") == 1 && KeepNormalMusic == false)
        {
            LevelMusic = OldTImeyMusic;
        }
        else if ((PlayerPrefs.GetInt("PartyModeON") == 0 && PlayerPrefs.GetInt("OldTimeyON") == 0) || KeepNormalMusic == true)
        {
            LevelMusic = NormalMusic;
        }
        if (BGMusic != null)
        {
            BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
        }
        else
        {
            BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
        }
        BGMusic.CompareSongs();

    }

    public void CheckControllers()
    {
        for (int i = 0; i < temp.Length; ++i) // go through the inputs
        {
            if (temp[i].ToString() == "Controller (Xbox One For Windows)" || temp[i].ToString() == "XInput Controller") // xbox controller match
            {
                ControllerModeEnabled = true;
                //Debug.Log("Xbox Controller connected");
                PS4Enabled = false;
                break;
            }
            else if (temp[i].ToString() == "Wireless Controller" || temp[i].ToString() == "PS4 Controller") // ps4 controller match
            {
                ControllerModeEnabled = true;
                //Debug.Log("ps4 Controller connected");
                PS4Enabled = true;
                break;
            }
            else // neither ps4 or xbox
            {
                ControllerModeEnabled = false;
                Debug.Log("No controller connected");
                PS4Enabled = false;
                RumbleEnabled = false;
            }
        }

    }


}
