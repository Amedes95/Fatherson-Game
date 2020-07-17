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



    // Start is called before the first frame update
    void OnEnable()
    {
        PrimaryIndex = 0;
        ToggleVisibility();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt(PrimaryHighlight.GetComponent<AchievementInfo>().AchievementTitle, 1);
            ToggleVisibility();
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

        //PrimaryHighlight.transform.localScale = MainHighlightPos.transform.localScale;
        //LeftHighlight.transform.localScale = LeftPreviewPos.transform.localScale;
        //RightHighlight.transform.localScale = RightPreviewPos.transform.localScale;

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
        Debug.Log(PrimaryHighlight);
        PrimaryHighlight.GetComponent<Animator>().SetBool("Active", true);


    }


}
