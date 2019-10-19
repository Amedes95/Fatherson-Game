using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class BlurryEffect : MonoBehaviour
{
    public PostProcessingProfile Transition1; // For blurry effect


    // Start is called before the first frame update
    void Start()
    {
        var BlurryScreen = Transition1.depthOfField.settings;
        BlurryScreen.focalLength = 300;
        Transition1.depthOfField.settings = BlurryScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        var BlurryScreen = Transition1.depthOfField.settings;
        BlurryScreen.focalLength = 0;
        Transition1.depthOfField.settings = BlurryScreen;
    }
}
