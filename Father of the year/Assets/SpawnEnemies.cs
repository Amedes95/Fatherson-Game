using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> EnemyWave;
    public GameObject PortalSpawnEffect;
    public static GameObject PortalClone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            CreateEnemies();
        }
    }

    public void CreateEnemies()
    {
        foreach (GameObject Enemy in EnemyWave)
        {
            Enemy.SetActive(true);
            PortalClone = Instantiate(PortalSpawnEffect, Enemy.transform.position, Quaternion.identity);
        }
    }
}
