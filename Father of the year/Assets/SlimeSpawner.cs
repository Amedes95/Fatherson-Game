using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    public float SpawnDelay;
    float TimerRestart;
    public GameObject SlimePrefab;
    public static GameObject SlimePrefabClone;

    public void Awake()
    {
        TimerRestart = SpawnDelay;
        SpawnDelay = .001f; // starat the spawning immediately
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SpawnDelay <= 0)
        {
            SpawnDelay = TimerRestart;
            SlimePrefabClone = Instantiate(SlimePrefab, transform.position, SlimePrefab.transform.rotation);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpawnDelay -= Time.smoothDeltaTime;
        }
    }
}
