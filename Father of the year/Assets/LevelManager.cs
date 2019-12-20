using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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


    AudioSource WorldHubAudioSource;
    public AudioClip ShiftRight;
    public AudioClip ShiftLeft;
    public AudioClip ShiftWorlds;


    public TextMeshPro WorldText;
    public TextMeshPro LevelText;


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
            //Debug.Log(PlayerPrefs.GetInt("Tutorial_Complete") + "Tutorial Complete");
            WorldsList.Add(World1);
        }
        if (WorldsList.Contains(World2) == false && PlayerPrefs.GetInt("World1_Complete") == 1)
        {
            WorldsList.Add(World2);
        }

    }


    // Update is called once per frame
    void Update()
    {
        ActiveWorld = WorldsList[WorldIndex];

        if (PauseScreen.GameIsPaused == false)
        {
            //// Shifts levels back and forth

            /// my mess that doesn't allow you to navigate over locked levels
            if (ActiveWorld.GetComponent<ListofLevels>().CurrentIndex < ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count-1)
            {
                if (ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[(ActiveWorld.GetComponent<ListofLevels>().CurrentIndex + 1)].GetComponent<LevelInfo>().Unlocked == true)
                {
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) // shift right
                    {
                        if (LevelIndex < ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1) // only shift if not the final one
                        {
                            ActiveWorld.transform.Translate(Vector3.left * ShiftDistance);
                            LevelIndex++;
                            WorldHubAudioSource.clip = ShiftWorlds;
                            WorldHubAudioSource.Play();
                        }
                        //Debug.Log(LevelIndex);
                        ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;

                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) // shift left
            {
                if (LevelIndex > 0)
                {
                    ActiveWorld.transform.Translate(Vector3.right * ShiftDistance);
                    LevelIndex--;
                    WorldHubAudioSource.clip = ShiftWorlds;
                    WorldHubAudioSource.Play();
                }
                //Debug.Log(LevelIndex);
                ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;
            }


            //// Shifts world indexer up and down
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // move down a world
            {
                if (WorldIndex > 0)
                {
                    WorldIndex--;
                    ResetBGs();
                    BackGroudSwapper();
                    WorldHubAudioSource.clip = ShiftLeft;
                    WorldHubAudioSource.Play();
                }
                //Debug.Log(WorldIndex);

            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // moves up
            {
                if (WorldIndex < WorldsList.Count - 1)
                {
                    WorldIndex++;
                    ResetBGs();
                    BackGroudSwapper();
                    WorldHubAudioSource.clip = ShiftRight;
                    WorldHubAudioSource.Play();

                }
                //Debug.Log(WorldIndex);
            }
            LevelIndex = ActiveWorld.GetComponent<ListofLevels>().CurrentIndex;


            


            // moves camera and selection box to active level
            Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, NewPos, .5f);
            SelectionBox.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);


            // loads scene when space is press and camera is at new pos
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) && Camera.transform.position == NewPos))
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
    }

    public void BackgroundChangeDirection(string Direction)
    {
        CenterBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        RightBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        LeftBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        TopBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
        BottomBG.GetComponent<BackgroundMove>().SwapDirections(Direction);
    }
}
