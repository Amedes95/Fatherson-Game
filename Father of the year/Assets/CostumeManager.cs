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




    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("CostumeIndex") == 0)
        {
            CostumeIndex = 0;
        }
        ToggleVsibility();
    }

    private void Awake()
    {
        CostumeIndex = PlayerPrefs.GetInt("CostumeIndex");
    }

    private void OnEnable()
    {
        CharacterSelectScreen.SetActive(true);
        CostumeIndex = PlayerPrefs.GetInt("CostumeIndex");
        ToggleVsibility();
    }
    private void OnDisable()
    {
        if (CharacterSelectScreen != null)
        {
            CharacterSelectScreen.SetActive(false);
        }

        if (CurrentCostume.GetComponent<CostumeInfo>().Locked) // locked costumes
        {
            PlayerPrefs.SetInt("CostumeIndex", 0); // default to normal costume if locked character is chosen
        }
        else
        {
            PlayerPrefs.SetInt("CostumeIndex", CostumeIndex); // 0, 1, etc..
        }


        //achievement for changing costime once
        if (PlayerPrefs.GetInt("CostumeIndex") != 0) // unlock if not default
        {
            /// Unlocks Fashionable Achievement
            if (PlayerPrefs.GetInt("Fashionable") == 0)
            {
                PlayerPrefs.SetInt("Fashionable", 1);
                Debug.Log("Fashionable Unlocked");
                Boombox Boombox = GameObject.FindGameObjectWithTag("LevelBoombox").GetComponent<Boombox>();
                Boombox.UnlockCheevo("Fashionable");
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
            Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
        else
        {
            CostumeIndex = 0;
            AbleToNavigate = false;
            Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
    }

    public void ShiftLeft()
    {
        if (CostumeIndex > 0)
        {
            CostumeIndex--;
            AbleToNavigate = false;
            Debug.Log(CostumeIndex);
            ToggleVsibility();
        }
        else
        {
            CostumeIndex = CostumesList.Count -1;
            AbleToNavigate = false;
            Debug.Log(CostumeIndex);
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
