using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CostumeManager : MonoBehaviour
{

    public List<GameObject> CostumesList;
    public GameObject CostumeBar;
    int CostumeIndex;
    GameObject CurrentCostume;

    bool EndOfList;
    bool StartOfList;
    bool AbleToNavigate;
    float NavigateTimer = .2f;

    public TextMeshPro CostumeDisplayName;

    public Transform PrimaryZone;

    public GameObject CharacterSelectScreen;

    int CostumeCount;




    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.PD.CostumeIndex == 0)
        {
            CostumeIndex = 0;
        }
        ToggleVsibility();
    }

    private void Awake()
    {
        CostumeIndex = PlayerData.PD.CostumeIndex;

    }

    private void OnEnable()
    {
        CharacterSelectScreen.SetActive(true);
        CostumeIndex = PlayerData.PD.CostumeIndex;
        ToggleVsibility();

        // achievement for unlocking all costumes
        if (PlayerData.PD.AchievementRecords.ContainsKey("Ancient Evil") && PlayerData.PD.AchievementRecords.ContainsKey("Flea Flee") && PlayerData.PD.AchievementRecords.ContainsKey("Fungus Among Us") && PlayerData.PD.AchievementRecords.ContainsKey("Ghastly Escape") && PlayerData.PD.AchievementRecords.ContainsKey("Party Crasher") && PlayerData.PD.AchievementRecords.ContainsKey("Fast Food") && PlayerData.PD.AchievementRecords.ContainsKey("Lucky 200") && PlayerData.PD.AchievementRecords.ContainsKey("Indigestible") && PlayerData.PD.AchievementRecords.ContainsKey("Insatiable Appetite") && PlayerData.PD.AchievementRecords.ContainsKey("Frostbitten") && PlayerData.PD.AchievementRecords.ContainsKey("Fossilized") && PlayerData.PD.AchievementRecords.ContainsKey("Spoiled Appetite")) // oops hard code, fuck it
        {
            /// Unlocks Fashion Statement Achievement
            if (PlayerData.PD.AchievementRecords.ContainsKey("Fashion Statement") == false) // not already unlocked?
            {
                PlayerData.PD.AchievementRecords.Add("Fashion Statement", 1); // add to unlock dictionary
                Debug.Log("Fashion Statement Unlocked");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Fashion Statement");
            }
        }

    }
    private void OnDisable()
    {
        if (CharacterSelectScreen != null)
        {
            CharacterSelectScreen.SetActive(false);
        }
        if (CurrentCostume.GetComponent<CostumeInfo>().Locked) // locked costumes
        {
            PlayerData.PD.CostumeIndex = 0; // default to normal costume if locked character is chosen
        }
        else
        {
            PlayerData.PD.CostumeIndex = CostumeIndex; // 0, 1, etc..
        }



        //achievement for changing costime once
        if (PlayerData.PD.CostumeIndex != 0) // unlock if not default
        {
            /// Unlocks Fashionable Achievement
            if (PlayerData.PD.AchievementRecords.ContainsKey("Fashionable") == false) // not already unlocked?
            {
                PlayerData.PD.AchievementRecords.Add("Fashionable", 1); // add to unlock dictionary
                Debug.Log("Fashionable Unlocked");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Fashionable");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCostume = CostumesList[CostumeIndex];
        if (CurrentCostume.GetComponent<CostumeInfo>().Locked)
        {
            CostumeDisplayName.text = CurrentCostume.GetComponent<CostumeInfo>().SecretName.ToString();
        }
        else
        {
            CostumeDisplayName.text = CurrentCostume.GetComponent<CostumeInfo>().CostumeName.ToString();
        }
    }

    public void ShiftRight()
    {
        if (CostumeIndex < CostumesList.Count - 1) // only shift right if not the final one
        {
            CostumeIndex++;
            AbleToNavigate = false;
            //Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
        else
        {
            CostumeIndex = 0;
            AbleToNavigate = false;
            //Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
    }

    public void ShiftLeft()
    {
        if (CostumeIndex > 0)
        {
            CostumeIndex--;
            AbleToNavigate = false;
            //Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
        else
        {
            CostumeIndex = CostumesList.Count -1;
            AbleToNavigate = false;
            //Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
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

    public void ToggleVsibility()
    {
        CurrentCostume = CostumesList[CostumeIndex];

        foreach (GameObject costume in CostumesList)
        {
            if (costume != CurrentCostume)
            {
                costume.SetActive(false);
            }
            else
            {
                costume.transform.position = PrimaryZone.position;
                costume.SetActive(true);
                if (CurrentCostume.GetComponent<CostumeInfo>().Locked)
                {
                    CostumeDisplayName.text = costume.GetComponent<CostumeInfo>().SecretName.ToString();
                }
                else
                {
                    CostumeDisplayName.text = costume.GetComponent<CostumeInfo>().CostumeName.ToString();
                }
            }
        }

    }
}
