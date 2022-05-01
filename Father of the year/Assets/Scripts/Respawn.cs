using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    Transform PlayerSpawn;
    int CurrentWorld;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "WorldHub") // if not in the hub, spawn normally
        {
            PlayerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").GetComponent<Transform>();
            transform.position = PlayerSpawn.position; // spawn
        }
        else // in the hub
        {
            CurrentWorld = PlayerData.CurrentWorld;
            GameObject[] Spawners = GameObject.FindGameObjectsWithTag("WorldSpawn");
            foreach (GameObject SpawnPoint in Spawners)
            {
                if (CurrentWorld == SpawnPoint.GetComponent<WorldSpawn>().WorldNumber)
                {
                    transform.position = SpawnPoint.transform.position; // spawn at the current world spawner
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
