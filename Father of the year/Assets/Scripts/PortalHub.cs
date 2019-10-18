using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PortalHub : MonoBehaviour
{
    public string SceneToLoad;
    public GameObject CompleteSymbol;
    public int WorldNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<Animator>().SetTrigger("Collapse");
            collision.gameObject.SetActive(false);
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

    public void LoadLevel()
    {
        PlayerPrefs.SetInt("CurrentWorld", WorldNumber); // update the current world for respawning later in the hub
        SceneManager.LoadScene(SceneToLoad);
    }
}
