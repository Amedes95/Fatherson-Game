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
        transform.position = FocusZone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraSpeed = PlayerMovement.playerVelocity / PlayerMovement.maxVelocity * .1f + .1f;
        FocusZoneFix = new Vector3(FocusZone.transform.position.x, FocusZone.transform.position.y, -10f);
        transform.position = Vector3.MoveTowards(transform.position, FocusZoneFix, CameraSpeed);
    }
}
