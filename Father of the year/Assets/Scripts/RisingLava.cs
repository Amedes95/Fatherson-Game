using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingLava : MonoBehaviour
{

    public static float RiseSpeed;
    public static bool Rising;
    PauseMenu PauseScreen;


    // Start is called before the first frame update
    void Awake()
    {
        PauseScreen = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Rising && PlayerHealth.Dead == false && PauseMenu.GameIsPaused == false)
        {
            Rise();
        }
    }

    public void Rise()
    {
        transform.parent.Translate(Vector2.up * RiseSpeed);
    }
}
