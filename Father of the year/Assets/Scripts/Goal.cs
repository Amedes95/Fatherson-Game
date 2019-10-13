using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public static bool LevelComplete;
    public GameObject VictoryMenu;
    public string NextLevel;

    // Start is called before the first frame update
    void Awake()
    {
        LevelComplete = false;
    }


    private void Update()
    {
        if (LevelComplete)
        {
            VictoryMenu.SetActive(true);
        }
        else
        {
            VictoryMenu.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            LevelComplete = true;
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
