using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixScreenRes : MonoBehaviour
{
    bool UltraWide;
    // Start is called before the first frame update
    void Start()
    {
        FixRatio();
    }

    // Update is called once per frame
    void Update()
    {
        var CurrentRatio = Screen.currentResolution;
        Debug.Log(CurrentRatio);
        if (CurrentRatio.width == 2560 || CurrentRatio.width == 3440 || CurrentRatio.width == 5120)
        {
            if (CurrentRatio.height == 1080 || CurrentRatio.height == 1440 || CurrentRatio.height == 2160)
            {
                FixRatio();
                UltraWide = true;
            }
            else { UltraWide = false; }
        }
        else { UltraWide = false; }
    }

    public void FixRatio()
    {
        var CurrentRatio = Screen.currentResolution;
        //gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(CurrentRatio.width, CurrentRatio.height);
        Screen.SetResolution(1920, 1080, true);
    }
}
