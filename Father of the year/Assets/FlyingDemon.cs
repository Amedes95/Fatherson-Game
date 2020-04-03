using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDemon : MonoBehaviour
{
    public GameObject LavaDropPrefab;
    public static GameObject LavaDropClone;
    public float DropRate;
    float DropRateCopy;
    public Transform LavaDropZone;
    public bool FlyingEnemy;

    // Start is called before the first frame update
    void Start()
    {
        DropRateCopy = DropRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FlyingEnemy)
        {
            if (DropRate > 0)
            {
                DropRate -= Time.smoothDeltaTime;
            }

            if (DropRate <= 0)
            {
                DropRate = DropRateCopy;
                SpawnLavaDrop();
            }
        }

    }

    public void SpawnLavaDrop()
    {
        LavaDropClone = Instantiate(LavaDropPrefab, LavaDropZone.position, transform.rotation);
    }



}
