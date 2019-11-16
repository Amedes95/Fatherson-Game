using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Transform RightLoopPos;
    public Transform LeftLoopPos;
    public Transform BottomLoopPos;
    public Transform UpwardLoopPos;

    Vector3 StartPos;

    float XLoopRight;
    float XLoopLeft;

    float YLoopUp;
    float YLoopDown;

    public bool Horizontal;
    public bool Vertical;
    public float Speed;

    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;

    private void Start()
    {
        StartPos = transform.position;

        XLoopRight = RightLoopPos.transform.position.x;
        XLoopLeft = LeftLoopPos.transform.position.x;

        YLoopUp = UpwardLoopPos.transform.position.y;
        YLoopDown = BottomLoopPos.transform.position.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Horizontal) /// Are you moving horizontal?
        {
            if (Left) // Moving left
            {
                transform.Translate(Vector3.left * Speed);

                if (transform.localPosition.x <= -80f)
                {
                    transform.position = new Vector3(XLoopRight, RightLoopPos.position.y, 0);
                }
            }
            if (Right) // Moving left
            {
                transform.Translate(Vector3.right * Speed);

                if (transform.localPosition.x >= 80f)
                {
                    transform.position = new Vector3(XLoopLeft, LeftLoopPos.position.y, 0);
                }
            }
        }
        if (Vertical)
        {
            if (Up)
            {
                transform.Translate(Vector3.up * Speed);

                if (transform.localPosition.y >= 79f)
                {
                    transform.position = new Vector3(UpwardLoopPos.position.x, YLoopDown, 0);
                }
            }
            if (Down)
            {
                transform.Translate(Vector3.down * Speed);

                if (transform.localPosition.y <= -79f)
                {
                    Debug.Log("???");
                    transform.position = new Vector3(BottomLoopPos.transform.position.x, YLoopUp, 0);
                }
            }
        }

    }

    public void ResetAllPositions()
    {
        transform.position = StartPos;
    }


    public void SwapDirections(string Direction)
    {
        if (Direction == "Right")
        {
            MoveRight();
        }
        else if (Direction == "Left")
        {
            MoveLeft();
        }
        else if (Direction == "Down")
        {
            MoveDownWards();
        }
        else if (Direction == "Up")
        {
            MoveUpwards();
        }
        else if (Direction == "STOP")
        {
            StopMoving();
        }
    }

    public void MoveUpwards()
    {
        Horizontal = false;
        Vertical = true;

        Up = true;
        Down = false;
        Left = false;
        Right = false;
    }

    public void MoveDownWards()
    {
        Horizontal = false;
        Vertical = true;

        Down = true;
        Up = false;
        Left = false;
        Right = false;
    }

    public void MoveRight()
    {
        Horizontal = true;
        Vertical = false;

        Right = true;
        Left = false;
        Up = false;
        Down = false;

    }

    public void MoveLeft()
    {
        Horizontal = true;
        Vertical = false;

        Left = true;
        Right = false;
        Down = false;
        Up = false;
    }

    public void StopMoving()
    {
        Horizontal = false;
        Vertical = false;

        Down = false;
        Up = false;
        Right = false;
        Left = false;
    }
}
