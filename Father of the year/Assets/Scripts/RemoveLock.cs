using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerData.Tutorial_Complete == 1) // If the level is complete it will be a 1
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
