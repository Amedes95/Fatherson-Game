using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public int TotalFruitCollected;
    public int FruitFromLevel;

    private void Awake()
    {
        TotalFruitCollected = PlayerPrefs.GetInt("FruitCollected"); // returns the saved value of collected fruits
    }

    public void AddToFruitStash()
    {
        TotalFruitCollected = PlayerPrefs.GetInt("FruitCollected"); // returns the saved value of collected fruits
        TotalFruitCollected += FruitFromLevel;
        PlayerPrefs.SetInt("FruitCollected", TotalFruitCollected);
    }


}
