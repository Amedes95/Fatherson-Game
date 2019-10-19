using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Preload : MonoBehaviour
{
    public string NameToDisplay;

    private void Awake()
    {
        GetComponentInChildren<Text>().text = NameToDisplay;
    }
}
