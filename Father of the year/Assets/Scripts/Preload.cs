using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Preload : MonoBehaviour
{
    public string NameToDisplay;

    private void Awake()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = NameToDisplay;
    }
}
