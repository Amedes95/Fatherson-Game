using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private GameObject FocusZone;
    float CameraSpeed;
    public float maxCameraSpeed;
    Vector3 FocusZoneFix;
    public bool cameraBounds;
    public Vector3 minCameraBounds;
    public Vector3 maxCameraBounds;
    bool cameraMoving;

    // Start is called before the first frame update
    void Start()
    {
        FocusZone = GameObject.FindGameObjectWithTag("FocusZone");
        transform.position = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraSpeed = Mathf.Min(PlayerMovement.playerVelocity / PlayerMovement.maxVelocity * maxCameraSpeed + .05f, maxCameraSpeed); // This scales the speed the camera moves towards the focus zone according to player velocity.
        FocusZoneFix = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f); // This has (x,y) coordinates of the focus zone with the proper z value (-10) for the camera. The camera bugs out if it's z position is not -10

        if (Vector3.Magnitude(FocusZoneFix - transform.position) > 12 || PlayerMovement.playerVelocity > 9)
        {
            cameraMoving = true;
        }
        else if (Vector3.Magnitude(FocusZoneFix - transform.position) < .01f && PlayerMovement.playerVelocity < .5f)
        {
            cameraMoving = false;
        }

        if (cameraMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, FocusZoneFix, CameraSpeed);
        } 
            Debug.Log(Vector3.Magnitude(FocusZoneFix - transform.position));

        if (cameraBounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraBounds.x, maxCameraBounds.x),
                Mathf.Clamp(transform.position.y, minCameraBounds.y, maxCameraBounds.y),
                -10);
        }
    }
}
