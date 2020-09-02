using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public TextMeshProUGUI AchievementNameText;
    public TextMeshProUGUI DescriptionText;
    public List<GameObject> Achievements;

    public Transform MainHighlightPos;
    public Transform LeftPreviewPos;
    public Transform RightPreviewPos;

    GameObject PrimaryHighlight;
    GameObject LeftHighlight;
    GameObject RightHighlight;

    int PrimaryIndex;
    int LeftIndex;
    int RightIndex;

    float NavigateTimer = .2f;
    bool AbleToNavigate;


    // Start is called before the first frame update
    void OnEnable()
    {
        PrimaryIndex = 0;
        ToggleVisibility();
        CheckCompletion();
    }

    // Update is called once per frame
    void Update()
    {
        PrimaryHighlight.transform.position = MainHighlightPos.position;
        LeftHighlight.transform.position = LeftPreviewPos.position;
        RightHighlight.transform.position = RightPreviewPos.position;

        if (PrimaryHighlight.GetComponent<AchievementInfo>().Secret && PrimaryHighlight.GetComponent<AchievementInfo>().Locked)
        {
            DescriptionText.text = "?????";
            AchievementNameText.text = "?????";
        }
        else
        {
            DescriptionText.text = PrimaryHighlight.GetComponent<AchievementInfo>().AchievementDescription;
            AchievementNameText.text = PrimaryHighlight.GetComponent<AchievementInfo>().AchievementTitle;
        }


        //// debug tool
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayerPrefs.SetInt(PrimaryHighlight.GetComponent<AchievementInfo>().AchievementTitle, 1);
        //    ToggleVisibility();
        //}
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && AbleToNavigate)
        {
            ScrollRight();
            AbleToNavigate = false;
        }
        else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && AbleToNavigate)
        {
            ScrollLeft();
            AbleToNavigate = false;
        }
    }

    public void ScrollRight()
    {

        if (PrimaryIndex != Achievements.Count - 1) // if you are not the last in the list
        {
            PrimaryIndex += 1;
        }
        else
        {
            PrimaryIndex = 0;
        }
        ToggleVisibility();
    }

    public void ScrollLeft()
    {
        if (PrimaryIndex != 0)
        {
            PrimaryIndex -= 1;
        }
        else
        {
            PrimaryIndex = Achievements.Count - 1;
        }
        ToggleVisibility();

    }

    public void ToggleVisibility()
    {
        PrimaryHighlight = Achievements[PrimaryIndex];
        PrimaryHighlight.GetComponent<Animator>().SetBool("Active", true);

        if (PrimaryIndex != 0 && PrimaryIndex != Achievements.Count - 1) // not left or rightmost node
        {
            LeftHighlight = Achievements[PrimaryIndex - 1];
            RightHighlight = Achievements[PrimaryIndex + 1];
        }

        if (PrimaryIndex == 0) // if the left most achievement is highlighted
        {
            LeftHighlight = Achievements[Achievements.Count - 1]; // the left preview is the rightmost one
            RightHighlight = Achievements[1];
        }
        else if (PrimaryIndex == Achievements.Count - 1)
        {
            RightHighlight = Achievements[0];
            LeftHighlight = Achievements[Achievements.Count - 2];
        }

        foreach (GameObject Achievement in Achievements)
        {
            if (Achievement == LeftHighlight || Achievement == RightHighlight || Achievement == PrimaryHighlight)
            {
                Achievement.SetActive(true);
                if (Achievement == PrimaryHighlight)
                {
                    Achievement.GetComponent<Animator>().SetBool("Active", true);
                }
                else
                {
                    Achievement.GetComponent<Animator>().SetBool("Active", false);
                }
            }
            else
            {
                Achievement.SetActive(false);
            }
        }
        //Debug.Log(PrimaryHighlight);
        PrimaryHighlight.GetComponent<Animator>().SetBool("Active", true);


    }


    private void FixedUpdate()
    {
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

    public void CheckCompletion()
    {
        int CheevosUnlocked = 0; // fresh count

        foreach (GameObject Cheevo in Achievements) // cycle all cheevos
        {
            if (PlayerPrefs.GetInt(Cheevo.GetComponent<AchievementInfo>().AchievementTitle) == 1) // am i unlocked? yes? + 1
            {
                CheevosUnlocked += 1;
            }
        }



        Debug.Log(CheevosUnlocked);
        if (CheevosUnlocked >= 38)
        {
            /// Unlocks Couch Potato Achievement
            if (PlayerPrefs.GetInt("Couch Potato") == 0)
            {
                PlayerPrefs.SetInt("Couch Potato", 1);
                Debug.Log("Couch Potato");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Couch Potato");
            }
        }
    }

}
