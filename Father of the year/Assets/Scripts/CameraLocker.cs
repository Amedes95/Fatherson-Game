using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocker : MonoBehaviour
{
    GameObject Camera;
    float DefaultZoon;
    public float CameraZoom;
    public bool TrapPlayer;
    public GameObject TrapperBounds;
    public bool ZoomCameraOut;

    // Start is called before the first frame update
    void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        DefaultZoon = Camera.GetComponent<CameraFollower>().cameraZoom; // save the level default zoom
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            Camera.GetComponent<CameraFollower>().FocusZone = transform.gameObject;
            Camera.GetComponent<CameraFollower>().minCameraBounds = transform.position;
            Camera.GetComponent<CameraFollower>().maxCameraBounds = transform.position;
            //Camera.GetComponent<Camera>().orthographicSize = CameraZoom;
            if (TrapPlayer)
            {
                TrapperBounds.SetActive(true);
            }
            if (Camera.GetComponent<Camera>().orthographicSize < CameraZoom && ZoomCameraOut)
            {
                Camera.GetComponent<Camera>().orthographicSize += .5f;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.activeInHierarchy)
        {
            Camera.GetComponent<CameraFollower>().FocusZone = collision.gameObject;
            //Camera.GetComponent<Camera>().orthographicSize = DefaultZoon;

        }
    }
}
