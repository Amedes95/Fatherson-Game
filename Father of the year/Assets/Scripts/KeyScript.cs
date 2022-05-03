using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KeyScript : MonoBehaviour
{

    public float size; //5 is a good size
    public GameObject KeyParticles;
    public static GameObject ParticlesCopy;
    public bool GoldenKey;
    public TextMeshPro GoldTrophyCount;

    // Start is called before the first frame update
    void Start()
    {
        size = size / transform.parent.transform.localScale.x;
        transform.localScale = new Vector3(size, size, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject.transform.parent.gameObject); // Using destroy instead because of fan logic
            //this.transform.parent.gameObject.SetActive(false);
            ParticlesCopy = Instantiate(KeyParticles, transform.position, Quaternion.identity);
            Destroy(ParticlesCopy, 3f); // destroys particles after 3 seconds
        }
    }

    private void Awake()
    {
        if (GoldenKey) // door to Gold City. Teleports key to player at the start of the level.
        {
            if (PlayerData.PD.AchievementRecords.ContainsKey("Gold Medalist")) // achievement is unlocked?
            {
                GameObject Player = GameObject.FindGameObjectWithTag("Player");
                transform.position = Player.transform.position;
            }
        }
    }

    private void Update()
    {
        if (GoldenKey)
        {
            GoldTrophyCount.text = PlayerData.PD.TotalGoldMedals.ToString();
        }
    }

}
