using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using Steamworks;


public class Goal : MonoBehaviour
{
    VictoryMenu VictoryScreen;
    public PostProcessingProfile Transition1;
    bool PulsingChroma;
    bool Rising;
    float BestTime;
    public bool Level1Portal;
    public bool EndPartyPortal;



    bool SpeedRunning;
    public float CompletionTime;

    public bool SpecialCredits;

    public bool VeganAlternative;
    public string VeganAlternativeLevel;
    int MalnourishedLives;


    // Start is called before the first frame update
    void Awake()
    {
        PlayerData.ExitedLevel = SceneManager.GetActiveScene().name; // lets keep track of what level you loaded into
        if (PlayerData.PlayerTimeRecords.ContainsKey(SceneManager.GetActiveScene().name)) // does this level have a record?
        {
            BestTime = PlayerData.GetLevelBestTime(SceneManager.GetActiveScene().name); // retrive that best time if it exists
        }
        SpeedRunning = true;
        CompletionTime = 0f;
        PulsingChroma = false;
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
        VictoryScreen.GoalReached = false;
        var Chroma = Transition1.chromaticAberration.settings;
        Chroma.intensity = 0;
        Transition1.chromaticAberration.settings = Chroma;
        if (Level1Portal)
        {
            PlayerPrefs.SetInt("Flawless Run", 0); // related to no deaths in a world achievement
            if (PlayerPrefs.GetInt("PartyModeON") == 1)
            {
                PlayerPrefs.SetInt("Party Run", 1);

            }
        }
        if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
        {
            MalnourishedLives = PlayerPrefs.GetInt("MalnourishedLives");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) // player completed level
    {
        if (collision.tag == "Player" && PauseMenu.GameIsPaused == false && PlayerHealth.Dead == false)
        {

            /// Unlocks Noobie Achievement
            if (PlayerData.AchievementRecords.ContainsKey("Noobie") == false) // not already unlocked?
            {
                PlayerData.AchievementRecords.Add("Noobie", 1);
                Debug.Log("Noobie Unlocked");
                BackgroundMusic BGMusic = GameObject.FindGameObjectWithTag("BGMusic").GetComponent<BackgroundMusic>();
                BGMusic.UnlockCheevo("Noobie");
            }
            collision.GetComponent<Collector>().AddToFruitStash();
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Animator>().SetTrigger("Complete");
            Boombox.SetVibrationIntensity(.5f, .25f, .25f);
            collision.gameObject.SetActive(false);

            ////// Compare best time with completion time for records
            SpeedRunning = false; // stop timer
            string ActiveSceneName = SceneManager.GetActiveScene().name;
            if (PlayerData.PlayerTimeRecords.ContainsKey(ActiveSceneName) == false) // if its your first run and no record exists already
            {
                PlayerData.PlayerTimeRecords.Add(ActiveSceneName, (float) CompletionTime); // update playerdata with your best time!
                Debug.Log("First timer" + PlayerData.GetLevelBestTime(ActiveSceneName));
            }
            else if (PlayerData.PlayerTimeRecords.ContainsKey(ActiveSceneName)) // did you do better? Check if entry exists already, then compare values
            {
                if (CompletionTime < BestTime) // level completed faster than previous record
                {
                    PlayerData.PlayerTimeRecords.Remove(ActiveSceneName); // remove old record, then add a new one with the same key
                    PlayerData.PlayerTimeRecords.Add(ActiveSceneName, (float)CompletionTime); // update playerdata with your NEW best time!
                    Debug.Log("New record" + PlayerData.GetLevelBestTime(ActiveSceneName));
                    // insert NEW BEST TIME sound effect here
                }

            }
            // show best time?

            if (SpecialCredits)
            {
                VictoryScreen.ExitToHub();
            }

            if (PlayerPrefs.GetInt("VeganMode") == 1) // you are on vegan mode, rapid complete the level
            {
                if (VeganAlternative) // am I one of the final levels?
                {
                    VictoryScreen.NextLevel = VeganAlternativeLevel;
                    VictoryScreen.LoadNextLevel();
                }
                else
                {
                    VictoryScreen.LoadNextLevel();
                }
            }
            if (PlayerPrefs.GetInt("MalnourishedMode") == 1)
            {
                MalnourishedLives += 3;
                PlayerPrefs.SetInt("MalnourishedLives", MalnourishedLives);
                Debug.Log("Malnourished lives" + PlayerPrefs.GetInt("MalnourishedLives"));
            }
        }
    }

    public void DisplayVictoryScreen()
    {
        VictoryScreen.GoalReached = true;
        var Chroma = Transition1.chromaticAberration.settings;
        if (PlayerPrefs.GetInt("ChromaON") == 0) // prefs start at 0
        {
            Chroma.intensity = 1;
            Transition1.chromaticAberration.settings = Chroma;
            PulsingChroma = true;
        }

    }

    private void Update()
    {
        var Chroma = Transition1.chromaticAberration.settings;
        if (PulsingChroma)
        {
            if (Chroma.intensity == 1f)
            {
                Rising = false;
            }
            else if (Chroma.intensity == 0f)
            {
                Rising = true;
            }

            if (Rising)
            {
                Chroma.intensity += .02f;
                Transition1.chromaticAberration.settings = Chroma;
                if (Chroma.intensity >= 1f)
                {
                    Chroma.intensity = 1f;
                    Rising = false;
                }
            }
            else if (Rising == false)
            {
                Chroma.intensity -= .02f;
                Transition1.chromaticAberration.settings = Chroma;
                if (Chroma.intensity <= 0f)
                {
                    Chroma.intensity = 0f;
                    Rising = true;
                }
            }




        }

    }

    private void FixedUpdate()
    {
        if (SpeedRunning)
        {
            CompletionTime += Time.smoothDeltaTime;
        }
    }
}
