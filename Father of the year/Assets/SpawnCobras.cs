using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCobras : MonoBehaviour
{
    public GameObject PurpleCobra;
    public static GameObject PurpleCobraClone;
    public GameObject RedCobra;
    public static GameObject RedCobraClone;
    public Transform SpawnZoneL;
    public Transform SpawnZoneR;
    public Transform SpawnZoneMid;
    public Transform SpawnZone4;
    public float SpawnDelayL;
    public float SpawnDelayR;
    public float SpawnDelayMid;
    public float SpawnDelay4;

    float SpawnLTime;
    float SpawnRTime;
    float SpawnMidTime;
    float Spawn4Time;
    bool Spawning;


    // Start is called before the first frame update
    void Start()
    {
        SpawnLTime = SpawnDelayL;
        SpawnRTime = SpawnDelayR;
        SpawnMidTime = SpawnDelayMid;
        Spawn4Time = SpawnDelay4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Spawning)
        {
            SpawnDelayL -= Time.smoothDeltaTime;
            SpawnDelayR -= Time.smoothDeltaTime;
            SpawnDelayMid -= Time.smoothDeltaTime;
            SpawnDelay4 -= Time.smoothDeltaTime;

            if (SpawnDelayL <= 0)
            {
                SpawnDelayL = SpawnLTime;
                PurpleCobraClone = Instantiate(PurpleCobra, SpawnZoneL.position, Quaternion.identity);
                PurpleCobraClone.GetComponent<BasicPatrol>().avoidsLedges = false;
            }
            if (SpawnDelayR <= 0)
            {
                SpawnDelayR = SpawnRTime;
                PurpleCobraClone = Instantiate(PurpleCobra, SpawnZoneR.position, Quaternion.identity);
                PurpleCobraClone.GetComponent<BasicPatrol>().avoidsLedges = false;
            }
            if (SpawnDelayMid <= 0)
            {
                SpawnDelayMid = SpawnMidTime;
                RedCobraClone = Instantiate(RedCobra, SpawnZoneMid.position, Quaternion.identity);
                RedCobraClone.GetComponent<BasicPatrol>().avoidsLedges = false;
            }
            if (SpawnDelay4 <= 0)
            {
                SpawnDelay4 = Spawn4Time;
                RedCobraClone = Instantiate(RedCobra, SpawnZone4.position, Quaternion.identity);
                RedCobraClone.GetComponent<BasicPatrol>().avoidsLedges = false;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Spawning = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Spawning = false;
        }
    }
}
