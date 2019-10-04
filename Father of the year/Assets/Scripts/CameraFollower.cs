using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject FocusZone;
    public float CameraSpeed;
    Vector3 FocusZoneFix;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FocusZoneFix = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
        transform.position = Vector3.MoveTowards(transform.position, FocusZoneFix, CameraSpeed);
    }
}
