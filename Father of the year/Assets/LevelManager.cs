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
        if (Input.GetKeyDown(KeyCode.A) && StartOfList == false)
        {
            ActiveWorld.transform.Translate(Vector3.right * ShiftDistance);
            LevelIndex--;
            if (LevelIndex < 0)
            {
                LevelIndex = 0;
            }
            Debug.Log(LevelIndex);
        }
        else if (Input.GetKeyDown(KeyCode.D) && EndOfList == false)
        {
            ActiveWorld.transform.Translate(Vector3.left * ShiftDistance);
            LevelIndex++;
            if (LevelIndex > ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1)
            {
                LevelIndex = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1;
            }
            Debug.Log(LevelIndex);
        }

        //// Shifts world indexer up and down
        if (Input.GetKeyDown(KeyCode.W)) // move down a world
        {
            WorldIndex--;
            LevelIndex = 0; // reset level index and position
            ActiveWorld.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);
            EndOfList = false;
            StartOfList = false;

            if (WorldIndex < 0)
            {
                WorldIndex = 0;
            }
            Debug.Log(WorldIndex);

        }
        else if (Input.GetKeyDown(KeyCode.S)) // moves up
        {
            WorldIndex++;
            LevelIndex = 0; // reset level index and position
            ActiveWorld.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);
            EndOfList = false;
            StartOfList = false;


            if (WorldIndex > WorldsList.Count - 1)
            {
                WorldIndex = WorldsList.Count - 1;
            }
            Debug.Log(WorldIndex);
        }


        // stops indexer from going too far right
        if (LevelIndex == ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1)
        {
            EndOfList = true;
        }
        else if (LevelIndex == 0) // too far left
        {
            StartOfList = true;
        }
        else
        {
            EndOfList = false;
            StartOfList = false;
        }


        // stops world indexer from going too far down 
        if (WorldIndex == ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld.Count - 1)
        {
            EndOfWorldList = true;
        }
        else if (WorldIndex == 0) // too far up
        {
            StartOfWorldList = true;
        }
        else
        {
            EndOfWorldList = false;
            StartOfWorldList = false;
        }


        // updates text on screen
        LevelText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().LevelDisplayName;
        WorldText.text = ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().WorldDisplayName;


        // moves camera and selection box to active level
        //Camera.transform.position = new Vector3(0, ActiveWorld.transform.position.y, -10);
        Vector3 NewPos = new Vector3(0, ActiveWorld.transform.position.y, -10);
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, NewPos, .5f);
        SelectionBox.transform.position = new Vector3(0, ActiveWorld.transform.position.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && Camera.transform.position == NewPos)
        {
            SceneManager.LoadScene(ActiveWorld.GetComponent<ListofLevels>().LevelsWithinWorld[LevelIndex].GetComponent<LevelInfo>().SceneToLoad);
        }

    }
}
