using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private GameObject FocusZone;
    float CameraSpeed;
    public float maxCameraSpeed;
    public float maxDistanceFromPlayer;
    Vector3 FocusZoneFix;
    public int cameraZoom;
    public bool cameraBounds;
    public Vector3 minCameraBounds;
    public Vector3 maxCameraBounds;
    bool cameraMoving;
    private Camera cameraComponent;

    // Start is called before the first frame update
    void Start()
    {
        FocusZone = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
        cameraComponent = gameObject.GetComponent<Camera>();
        cameraComponent.orthographicSize = cameraZoom;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraSpeed = Mathf.Min(Mathf.Abs(PlayerMovement.playerVelocity.x) / PlayerMovement.maxVelocity * maxCameraSpeed + .05f, maxCameraSpeed); // This scales the speed the camera moves towards the focus zone according to player velocity.
        FocusZoneFix = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f); // This has (x,y) coordinates of the focus zone with the proper z value (-10) for the camera. The camera bugs out if it's z position is not -10

        if (Vector3.Magnitude(FocusZoneFix - transform.position) > maxDistanceFromPlayer || Mathf.Abs(PlayerMovement.playerVelocity.x) >= 8 || Mathf.Abs(PlayerMovement.playerVelocity.y) > 9)
        {
            cameraMoving = true;
        }
        else if (Vector3.Magnitude(FocusZoneFix - transform.position) < 1f
            //&& PlayerMovement.playerVelocity.magnitude < .01f
            )
        {
            cameraMoving = false;
        }

        if (cameraMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, FocusZoneFix, CameraSpeed);
        }

        if (cameraBounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraBounds.x, maxCameraBounds.x),
                Mathf.Clamp(transform.position.y, minCameraBounds.y, maxCameraBounds.y),
                -10);
        }
    }
}
