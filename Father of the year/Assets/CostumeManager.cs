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
    }
    private void OnDisable()
    {
        if (CharacterSelectScreen != null)
        {
            CharacterSelectScreen.SetActive(false);
        }
        PlayerPrefs.SetInt("CostumeIndex", CostumeIndex); // 0, 1, etc..
    }

    // Update is called once per frame
    void Update()
    {
        CurrentCostume = CostumesList[CostumeIndex];
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
                CostumeDisplayName.text = costume.GetComponent<CostumeInfo>().CostumeName.ToString();
            }
        }
    }
}
