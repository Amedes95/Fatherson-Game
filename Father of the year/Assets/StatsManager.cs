using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public GameObject VeganConfirmation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AskToConfirmVeganMode()
    {
        VeganConfirmation.SetActive(true);
    }
    public void BeginVeganMode()
    {
        PlayerPrefs.SetInt("VeganMode", 1); // toggle bool
        PlayerPrefs.SetFloat("VeganTimer", 0); // reset timer

        SceneManager.LoadScene("Tutorial_01");
    }

    public void DenyVeganConfirmation()
    {
        VeganConfirmation.SetActive(false);
    }
}
