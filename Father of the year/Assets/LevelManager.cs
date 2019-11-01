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

    ////////
    public List<GameObject> WorldsList;
    GameObject ActiveWorld;

    int LevelIndex;
    int WorldIndex;

    bool EndOfList;
    bool StartOfList;
    bool EndOfWorldList;
    bool StartOfWorldList;

    public float ShiftDistance; // should be 10


    public TextMeshPro WorldText;
    public TextMeshPro LevelText;


    // Start is called before the first frame update
    void Awake()
    {
        WorldIndex = 0;
        ActiveWorld = WorldsList[WorldIndex];
    }

    // Update is called once per frame
    void Update()
    {
        ActiveWorld = WorldsList[WorldIndex];

        //// Shifts levels back and forth
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (LevelIndex > 0)
            {
                ActiveWorld.transform.Translate(Vector3.right * ShiftDistance);
                LevelIndex--;
            }
            //Debug.Log(LevelIndex);
            ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (LevelIndex < ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1)
            {
                ActiveWorld.transform.Translate(Vector3.left * ShiftDistance);
                LevelIndex++;
            }
            //Debug.Log(LevelIndex);
            ActiveWorld.GetComponent<ListofLevels>().CurrentIndex = LevelIndex;

        }

        //// Shifts world indexer up and down
        if (Input.GetKeyDown(KeyCode.W)) // move down a world
        {
            if (WorldIndex > 0)
            {
                WorldIndex--;
            }
            //Debug.Log(WorldIndex);

        }
        else if (Input.GetKeyDown(KeyCode.S)) // moves up
        {
            if (WorldIndex < WorldsList.Count - 1)
            {
                WorldIndex++;
            }
            //Debug.Log(WorldIndex);
        }
        LevelIndex = ActiveWorld.GetComponent<ListofLevels>().CurrentIndex;


        // updates text on screen
        LevelText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().LevelDisplayName;
        WorldText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().WorldDisplayName;


        // moves camera and selection box to active level
        Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, NewPos, .5f);
        SelectionBox.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);


        // loads scene when space is press and camera is at new pos
        if (Input.GetKeyDown(KeyCode.Space) && Camera.transform.position == NewPos)
        {
            SceneManager.LoadScene(ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().SceneToLoad);
        }

    }
}
