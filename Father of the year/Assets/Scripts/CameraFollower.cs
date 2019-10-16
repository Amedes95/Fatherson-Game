using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject FocusZone;
    float CameraSpeed;
    public float maxCameraSpeed;
    Vector3 FocusZoneFix;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        CameraSpeed = Mathf.Min(PlayerMovement.playerVelocity / PlayerMovement.maxVelocity * maxCameraSpeed + .05f, maxCameraSpeed);
        FocusZoneFix = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
        transform.position = Vector3.MoveTowards(transform.position, FocusZoneFix, CameraSpeed);
    }
}
