using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalHub : MonoBehaviour
{
    public string SceneToLoad;
    public GameObject CompleteSymbol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
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
