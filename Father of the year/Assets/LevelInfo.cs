using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public string LevelDisplayName;
    public string WorldDisplayName;
    public string SceneToLoad;
    public GameObject CompleteSymbol;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Active");
        gameObject.GetComponent<Animator>().SetBool("Active", true);

        //// for completion
        if (PlayerPrefs.GetInt(SceneToLoad) == 1) // 1 true, 0 false
        {
            CompleteSymbol.SetActive(true);
        }
        else
        {
            CompleteSymbol.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Animator>().SetBool("Active", false);
    }

    private void Update()
    {

    }
}
