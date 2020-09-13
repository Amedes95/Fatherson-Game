using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;


public class GhostBoss : MonoBehaviour
{
    public GameObject Knight;
    public PostProcessingProfile Transition1; // Blurry Screen shriek
    public GameObject ShriekEffect;

    // Start is called before the first frame update
    void Awake()
    {
        RisingLava.Rising = false;

        // used with shriek
        var Blurry = Transition1.depthOfField.settings;
        Blurry.useCameraFov = false;
        Transition1.depthOfField.settings = Blurry;
    }

    private void OnDisable() // mainly for shriek
    {
        var Blurry = Transition1.depthOfField.settings;
        Blurry.useCameraFov = false;
        Transition1.depthOfField.settings = Blurry;
        ShriekEffect.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void ActivateLavaRise()
    {
        RisingLava.Rising = true;
    }

    public void PauseLavaRise()
    {
        RisingLava.Rising = false;
    }

    public void SpawnKnight()
    {
        Knight.SetActive(true);
    }

    public void BlurryShriek()
    {
        var Blurry = Transition1.depthOfField.settings;
        if (Blurry.useCameraFov == true)
        {
            Blurry.useCameraFov = false;
            Transition1.depthOfField.settings = Blurry;

        }
        else
        {
            Blurry.useCameraFov = true;
            Transition1.depthOfField.settings = Blurry;
        }
    }

    public void BeginShriek()
    {
        ShriekEffect.SetActive(true);
    }

    public void Hide()
    {
        ShriekEffect.SetActive(false);

    }

    public void PlayShriekNoise()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

}
