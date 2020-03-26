using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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



    // Start is called before the first frame update
    void Awake()
    {
        WorldHubAudioSource = gameObject.GetComponent<AudioSource>();
        PauseScreen = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseMenu>();
        WorldIndex = 0;
        ActiveWorld = WorldsList[WorldIndex];
        /// unlocks levels
        if (WorldsList.Contains(World1) == false && PlayerPrefs.GetInt("Tutorial_Complete") == 1)
        {
            WorldsList.Add(World1);
        }
        if (WorldsList.Contains(World2) == false && PlayerPrefs.GetInt("World1_Complete") == 1)
        {
            WorldsList.Add(World2);
        }
        if (WorldsList.Contains(World3) == false && PlayerPrefs.GetInt("World2_Complete") == 1)
        {
            WorldsList.Add(World3);
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
            SpaceBarText.SetActive(false);
            AText.SetActive(true);
        }
        else
        {
            NavAxis = "UINavigate2";
            VerticalAxis = "UINavigate2Vertical";
            SpaceBarText.SetActive(true);
            AText.SetActive(false);
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


            //// Shifts world indexer up and down
            if (Input.GetAxis(VerticalAxis) == -1 && AbleToNavigate) // move down a world
            {
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
            else if (Input.GetAxis(VerticalAxis) == 1 && AbleToNavigate) // moves up
            {
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
            Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, NewPos, .5f);
            SelectionBox.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);


            // loads scene when space is press and camera is at new pos
            if ((Input.GetButtonDown("Submit")) && Camera.transform.position == NewPos)
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
        string ExitedLevel = PlayerPrefs.GetString("ExitedLevel"); // we got this from the Goal script
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
                    Debug.Log("World Index: " + WorldIndex);
                    ActiveWorld = WorldsList[WorldIndex]; // and the active world
                    BackGroudSwapper();

                }
            }
        }

    }

    public void UnlockAllWorlds()
    {
        PlayerPrefs.SetInt("Tutorial_Complete", 1);
        PlayerPrefs.SetInt("World1_Complete", 1);
        PlayerPrefs.SetInt("World2_Complete", 1);
        PlayerPrefs.SetInt("World3_Complete", 1);
        WorldsList.Add(World1);
        WorldsList.Add(World2);
        WorldsList.Add(World3);

    }

}
