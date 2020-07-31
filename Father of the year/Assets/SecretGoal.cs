using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretGoal : MonoBehaviour
{

    VictoryMenu VictoryScreen;
    //public string SecretLevelName;
    public bool PartyPortal;


    // Start is called before the first frame update
    void Awake()
    {
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryMenu").GetComponent<VictoryMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PartyPortal) // party portal, yeah baby
            {
                VictoryScreen.NextLevel = "Party01";
            }
            gameObject.GetComponent<Animator>().SetTrigger("Complete");
            Boombox.SetVibrationIntensity(.5f, .25f, .25f);
            collision.gameObject.SetActive(false);
            VictoryScreen.LoadNextLevel();
        }

    }
}
