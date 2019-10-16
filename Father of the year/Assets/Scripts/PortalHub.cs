using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalHub : MonoBehaviour
{
    public string SceneToLoad;
    public GameObject CompleteSymbol;
    public int CurrentWorld;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("CurrentWorld", CurrentWorld); // update the current world for respawning later in the hub
            SceneManager.LoadScene(SceneToLoad);
        }
    }


    private void Update()
    {
        if (PlayerPrefs.GetInt(SceneToLoad) == 1) // 1 true, 0 false
        {
            CompleteSymbol.SetActive(true);
        }
        else
        {
            CompleteSymbol.SetActive(false);
        }
    }
}
