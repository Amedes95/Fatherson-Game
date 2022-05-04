using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.PostProcessing;

public class LevelManager : MonoBehaviour
{

    public GameObject Camera;
    public GameObject SelectionBox;

    PauseMenu PauseScreen;

    ////////
    public List<GameObject> WorldsList;
    GameObject ActiveWorld;

    public GameObject World1;
    public GameObject World2;
    public GameObject World3;
    public GameObject World4;
    public GameObject World5;
    public GameObject World6;

    int LevelIndex;
    int WorldIndex;

    public GameObject DownArrow;
    public GameObject UpArrow;

    bool EndOfList;
    bool StartOfList;
    bool EndOfWorldList;
    bool StartOfWorldList;

    public float ShiftDistance; // should be 10

    public GameObject CenterBG;
    public GameObject RightBG;
    public GameObject LeftBG;
    public GameObject BottomBG;
    public GameObject TopBG;

    public Sprite TutorialBackground;
    public Sprite World1Background;
    public Sprite World2Background;
    public Sprite World3Background;
    public Sprite World4Background;
    public Sprite World5Background;
    public Sprite World6Background;



    AudioSource WorldHubAudioSource;
    public AudioClip ShiftRight;
    public AudioClip ShiftLeft;
    public AudioClip ShiftWorlds;


    public TextMeshPro WorldText;
    public TextMeshPro LevelText;

    float NavigateTimer = .2f;
    bool AbleToNavigate;

    string NavAxis;
    string VerticalAxis;

    public GameObject SpaceBarText;
    public GameObject AText;
    public GameObject XText;
    StandaloneInputModule Inputs;


    public PostProcessingProfile Transition1;

    public int GoldMedalsEarned;
    float MinorDelay = .2f;




    // Start is called before the first frame update
    void Awake()
    {
        PlayerData.PD.SavePlayer();
        VisualsScreen.Partying = false;
        VisualsScreen.BeingOld = false;

        PlayerPrefs.SetInt("Flawless Run", 1); // voids achievement if exited.  Only resets on start of level 1
        PlayerPrefs.SetInt("Party Run", 0); // cancel party run, no cheating
        PlayerPrefs.SetInt("VeganMode", 0); // cancel vegan mode if exited
        PlayerPrefs.SetFloat("VeganTimer", 0);

        PlayerPrefs.SetInt("MalnourishedMode", 0); // cancel if quit
        PlayerPrefs.SetInt("MalnourishedLives", 0);

        PlayerPrefs.SetInt("BossRush", 0); // cancel boss rush

        if (PlayerPrefs.GetInt("PartyModeON") == 1 && PlayerData.PD.PartyUnlocked == 0)
        {
            Debug.Log("Toggle");
            TogglePartyMode();
        }
        else if (PlayerPrefs.GetInt("OldTimeyON") == 1 && PlayerData.PD.OldTimeyUnlocked == 0)
        {
            ToggleOldTimerMode();
        }


        WorldHubAudioSource = gameObject.GetComponent<AudioSource>();
        PauseScreen = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseMenu>();
        WorldIndex = 0;
        ActiveWorld = WorldsList[WorldIndex];
        /// unlocks levels
        if (WorldsList.Contains(World1) == false && PlayerData.PD.Tutorial_Complete == 1)
        {
            WorldsList.Add(World1);
        }
        if (WorldsList.Contains(World2) == false && PlayerData.PD.World1_Complete == 1)
        {
            WorldsList.Add(World2);
        }
        if (WorldsList.Contains(World3) == false && PlayerData.PD.World2_Complete == 1)
        {
            WorldsList.Add(World3);
        }
        if (WorldsList.Contains(World4) == false && PlayerData.PD.World3_Complete == 1)
        {
            WorldsList.Add(World4);
        }
        if (WorldsList.Contains(World5) == false && PlayerData.PD.World4_Complete == 1)
        {
            WorldsList.Add(World5);
        }
        if (WorldsList.Contains(World6) == false && PlayerData.PD.World5_Complete == 1)
        {
            WorldsList.Add(World6);
        }


        /// add check for world 2 completion here (final level script)



        LoadCurrentWorld();

    }

    // Update is called once per frame
    void Update()
    {

        if (Boombox.ControllerModeEnabled)
        {
            NavAxis = "UINavigateHorizontal";
            VerticalAxis = "UINavigateVertical";

            if (Boombox.PS4Enabled)
            {
                AText.SetActive(false);
                XText.SetActive(true);

                if (Application.platform != (RuntimePlatform.LinuxPlayer) && Application.platform != (RuntimePlatform.LinuxEditor)) // don't change for Linux
                {
                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    Inputs.submitButton = "PS4Submit";
                }
                else
                {
                    Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                    Inputs.submitButton = "Submit";
                }


            }
            else
            {
                Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
                Inputs.submitButton = "Submit";
                AText.SetActive(true);
                XText.SetActive(false);
            }
            SpaceBarText.SetActive(false);
        }
        else
        {
            NavAxis = "UINavigate2";
            VerticalAxis = "UINavigate2Vertical";
            SpaceBarText.SetActive(true);
            AText.SetActive(false);
            XText.SetActive(false);
            Inputs = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<StandaloneInputModule>();
            Inputs.submitButton = "Submit";
        }

        ActiveWorld = WorldsList[WorldIndex];

        if (PauseMenu.GameIsPaused == false)
        {
            //// Shifts levels back and forth

            /// my mess that doesn't allow you to navigate over locked levels
            if (ActiveWorld.GetComponent<ListofLevels>().CurrentIndex < ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count-1)
            {
                if (ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[(ActiveWorld.GetComponent<ListofLevels>().CurrentIndex + 1)].GetComponent<LevelInfo>().Unlocked == true)
                {
                    if (Input.GetAxis(NavAxis) == 1 && AbleToNavigate) // shift right
                    { 
                        if (LevelIndex < ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1) // only shift if not the final one
                        {
                            ActiveWorld.transform.Translate(Vector3.left * ShiftDistance);
                            LevelIndex++;
                            WorldHubAudioSource.clip = ShiftWorlds;
                            WorldHubAudioSource.Play();
                            AbleToNavigate = false;
                        }
                        //Debug.Log(LevelIndex);
                        ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;

                    }
                }
            }

            if (Input.GetAxis(NavAxis) == -1 && AbleToNavigate) // shift left
            {
                PlayerPrefs.SetInt("LevelTimeBackup", 1); // never speak of this again
                if (LevelIndex > 0)
                {
                    ActiveWorld.transform.Translate(Vector3.right * ShiftDistance);
                    LevelIndex--;
                    WorldHubAudioSource.clip = ShiftWorlds;
                    WorldHubAudioSource.Play();
                    AbleToNavigate = false;
                }
                //Debug.Log(LevelIndex);
                ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;
            }

            Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
            //// Shifts world indexer up and down
            if (Input.GetAxis(VerticalAxis) == -1 && AbleToNavigate && Camera.transform.position == NewPos) // move down a world
            {
                PlayerPrefs.SetInt("LevelTimeBackup", 1); // never speak of this again
                if (WorldIndex > 0)
                {
                    WorldIndex--;
                    ResetBGs();
                    BackGroudSwapper();
                    WorldHubAudioSource.clip = ShiftLeft;
                    WorldHubAudioSource.Play();
                    AbleToNavigate = false;
                }
                //Debug.Log(WorldIndex);

            }
            else if (Input.GetAxis(VerticalAxis) == 1 && AbleToNavigate && Camera.transform.position == NewPos) // moves up
            {
                PlayerPrefs.SetInt("LevelTimeBackup", 1); // never speak of this again
                if (WorldIndex < WorldsList.Count - 1)
                {
                    WorldIndex++;
                    ResetBGs();
                    BackGroudSwapper();
                    WorldHubAudioSource.clip = ShiftRight;
                    WorldHubAudioSource.Play();
                    AbleToNavigate = false;

                }
                //Debug.Log(WorldIndex);
            }
            LevelIndex = ActiveWorld.GetComponent<ListofLevels>().CurrentIndex;


            


            // moves camera and selection box to active level
            //Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, NewPos, .5f);
            SelectionBox.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);


            // loads scene when space is press and camera is at new pos
            if ((Input.GetButtonDown(Inputs.submitButton.ToString())) && Camera.transform.position == NewPos)
            {
                if (ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().Unlocked)
                {
                    SceneManager.LoadScene(ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().SceneToLoad);
                }
            }
        }
 

        //// Activates up and down arrows depending on screen position
        if (WorldIndex == 0) // at the top (tutorial)
        {
            UpArrow.SetActive(false);
        }
        else
        {
            UpArrow.SetActive(true);
        }
        if (WorldIndex == WorldsList.Count - 1) // Bottom (last world)
        {
            DownArrow.SetActive(false);
        }
        else
        {
            DownArrow.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        // updates text on screen
        LevelText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().LevelDisplayName;
        WorldText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().WorldDisplayName;
        if (AbleToNavigate == false)
        {
            NavigateTimer -= Time.smoothDeltaTime;
            if (NavigateTimer <= 0)
            {
                AbleToNavigate = true;
                NavigateTimer = .2f;
            }
        }
        if (MinorDelay > 0)
        {
            MinorDelay -= Time.smoothDeltaTime;
            if (MinorDelay <= 0)
            {
                CheckForGolds();
                MinorDelay = 0;
            }
        }
    }

    public void ResetBGs()
    {
        CenterBG.GetComponent<BackgroundMove>().ResetAllPositions();
        RightBG.GetComponent<BackgroundMove>().ResetAllPositions();
        LeftBG.GetComponent<BackgroundMove>().ResetAllPositions();
        TopBG.GetComponent<BackgroundMove>().ResetAllPositions();
        BottomBG.GetComponent<BackgroundMove>().ResetAllPositions();
    }

    public void BackGroudSwapper()
    {
        /// background changes
        if (WorldIndex == 0) // Gray and black tiles
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = TutorialBackground;
            RightBG.GetComponent<SpriteRenderer>().sprite = TutorialBackground;
            LeftBG.GetComponent<SpriteRenderer>().sprite = TutorialBackground;
            TopBG.GetComponent<SpriteRenderer>().sprite = TutorialBackground;
            BottomBG.GetComponent<SpriteRenderer>().sprite = TutorialBackground;

            BackgroundChangeDirection("Right");

        }
        else if (WorldIndex == 1) // Orange arrows
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World1Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World1Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World1Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World1Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World1Background;

            BackgroundChangeDirection("Up");

        }
        else if (WorldIndex == 2) // green squares
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World2Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World2Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World2Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World2Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World2Background;

            BackgroundChangeDirection("Left");

        }
        else if (WorldIndex == 3) // purple squares
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World3Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World3Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World3Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World3Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World3Background;

            BackgroundChangeDirection("Down");

        }
        else if (WorldIndex == 4) // pink lines
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World4Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World4Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World4Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World4Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World4Background;

            BackgroundChangeDirection("Right");

        }
        else if (WorldIndex == 5) // blue squares
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World5Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World5Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World5Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World5Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World5Background;

            BackgroundChangeDirection("Up");

        }
        else if (WorldIndex == 6) // white squares
        {
            CenterBG.GetComponent<SpriteRenderer>().sprite = World6Background;
            RightBG.GetComponent<SpriteRenderer>().sprite = World6Background;
            LeftBG.GetComponent<SpriteRenderer>().sprite = World6Background;
            TopBG.GetComponent<SpriteRenderer>().sprite = World6Background;
            BottomBG.GetComponent<SpriteRenderer>().sprite = World6Background;

            BackgroundChangeDirection("Right");

        }


    }

    public void BackgroundChangeDirection(string Direction)
    {
        CenterBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        RightBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        LeftBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        TopBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        BottomBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
    }

    public void LoadCurrentWorld()
    {
        string ExitedLevel = PlayerData.PD.ExitedLevel; // we got this from the Goal script
        //Debug.Log(ExitedLevel);
        foreach (GameObject World in WorldsList) // check every level of every world
        {
            foreach (GameObject Level in World.GetComponent<ListofLevels>().LevelsWithinWorld) // foreach level in the array contained by the world
            {
                if (Level.GetComponent<LevelInfo>().SceneToLoad == ExitedLevel) // do we have a match?
                {
                    //Debug.Log("We got a match!");
                    float LevelXPos = Level.transform.position.x; // get the info so we can move the selection box
                    int StartingLevelIndex = World.GetComponent<ListofLevels>().LevelsWithinWorld.IndexOf(Level); // get the index of the level
                    float WorldYPos = World.transform.position.y; // get the Y pos of the world its contained in


                    // then move the items accordingly
                    SelectionBox.transform.position = new Vector3(0, WorldYPos, 0); // update selection box
                    Vector3 StartPos = new Vector3(0, WorldYPos, -10);
                    Camera.transform.position = StartPos; // update camera

                    // update the world pos
                    World.transform.position = new Vector3(LevelXPos * -1, World.transform.position.y, 0);
                    World.GetComponent<ListofLevels>().CurrentIndex = StartingLevelIndex;

                    WorldIndex = WorldsList.IndexOf(World); // and the world index
                    //Debug.Log("World Index: " + WorldIndex);
                    ActiveWorld = WorldsList[WorldIndex]; // and the active world
                    BackGroudSwapper();

                }
            }
        }

    }

    public void UnlockAllWorlds()
    {
        PlayerData.PD.Tutorial_Complete = 1;
        PlayerData.PD.World1_Complete = 1;
        PlayerData.PD.World2_Complete = 1;
        PlayerData.PD.World3_Complete = 1;
        PlayerData.PD.World4_Complete = 1;
        PlayerData.PD.World5_Complete = 1;
        PlayerData.PD.World6_Complete = 1;

        WorldsList.Add(World1);
        WorldsList.Add(World2);
        WorldsList.Add(World3);
        WorldsList.Add(World4);
        WorldsList.Add(World5);
        WorldsList.Add(World6);
    }

    public void LockAllWorlds()
    {
        PlayerData.PD.Tutorial_Complete = 0;
        PlayerData.PD.World1_Complete = 0;
        PlayerData.PD.World2_Complete = 0;
        PlayerData.PD.World3_Complete = 0;
        PlayerData.PD.World4_Complete = 0;
        PlayerData.PD.World5_Complete = 0;
        PlayerData.PD.World6_Complete = 0;

        WorldsList.Remove(World1);
        WorldsList.Remove(World2);
        WorldsList.Remove(World3);
        WorldsList.Remove(World4);
        WorldsList.Remove(World5);
        WorldsList.Remove(World6);
    }

    public void TogglePartyMode()
    {
        if (PlayerPrefs.GetInt("OldTimeyON") == 1)
        {
            ToggleOldTimerMode();
        }
        PlayerPrefs.SetInt("OldTimeyON", 0); // toggle off old timey mode
        if (PlayerPrefs.GetInt("PartyModeON") == 1) // turn party off... :(
        {
            PlayerPrefs.SetInt("PartyModeON", 0);
            PlayerPrefs.SetInt("Party Run", 0); // cancel party run, no cheating
        }
        else
        {
            PlayerPrefs.SetInt("PartyModeON", 1); // Party on baby
            var Hue = Transition1.colorGrading.settings;
            Hue.basic.hueShift = 180;
        }
        // helps change the music
        Boombox CurrentBoombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
        CurrentBoombox.UpdateSound();
    }
    public void ToggleOldTimerMode()
    {
        if (PlayerPrefs.GetInt("PartyModeON") == 1) // toggle off party mode
        {
            TogglePartyMode();
        }

        if (PlayerPrefs.GetInt("OldTimeyON") == 1)
        {
            PlayerPrefs.SetInt("OldTimeyON", 0);
            PlayerPrefs.SetInt("Party Run", 0); // cancel party run
            var Grainy = Transition1.grain.settings;
            Grainy.intensity = 0;
            Grainy.size = 3;
            Transition1.grain.settings = Grainy;
        }
        else
        {
            PlayerPrefs.SetInt("OldTimeyON", 1);
            var Hue = Transition1.colorGrading.settings;
            Hue.basic.hueShift = 7;
        }
        // helps change the music
        Boombox CurrentBoombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
        CurrentBoombox.UpdateSound();
    }

    public void CheckForGolds()
    {
        GoldMedalsEarned = 0;

        foreach (GameObject World in WorldsList)
        {
            //Debug.Log("Worlds:" + World);
            List<GameObject> WorldLevels = World.GetComponent<ListofLevels>().LevelsWithinWorld;
            foreach (GameObject Level in WorldLevels)
            {
                //Debug.Log("levels:" + Level.name);

                if (Level.GetComponent<LevelInfo>().GoldTierAchieved)
                {
                    //PlayerPrefs.SetInt("GoldMedalsEarned", GoldMedalsEarned);
                    GoldMedalsEarned += 1;
                }

            }
        }

        /// Unlocks Oooh Shiny! Achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("Oooh Shiny!") == false && GoldMedalsEarned >= 1) // not already unlocked?
        {
            PlayerData.PD.AchievementRecords.Add("Oooh Shiny!", 1); // add to unlock dictionary
            Debug.Log("Oooh Shiny! Unlocked");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Oooh Shiny!");
        }
        /// Unlocks Gold Medalist Achievement
        if (PlayerData.PD.AchievementRecords.ContainsKey("Gold Medalist") == false && GoldMedalsEarned >= 140) // not already unlocked?
        {
            PlayerData.PD.AchievementRecords.Add("Gold Medalist", 1);
            Debug.Log("Gold Medalist Unlocked");
            BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
            BGMusic.UnlockCheevo("Gold Medalist");
        }
        PlayerData.PD.TotalGoldMedals = GoldMedalsEarned; // store my medals please!
        //Debug.Log(GoldMedalsEarned);
    }


}
