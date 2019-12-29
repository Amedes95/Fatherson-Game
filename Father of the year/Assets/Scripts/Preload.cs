using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Preload : MonoBehaviour
{
    public string NameToDisplay;
    public string LevelNumberDisplay;

    public TextMeshProUGUI WorldText;
    public TextMeshProUGUI LevelText;

    private void Awake()
    {
        WorldText.text = NameToDisplay;
        LevelText.text = LevelNumberDisplay;

    }
}
