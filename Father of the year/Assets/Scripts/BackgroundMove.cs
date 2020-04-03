using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public Transform RightLoopPos;
    public Transform LeftLoopPos;
    public Transform BottomLoopPos;
    public Transform UpwardLoopPos;
    public Transform CenterTile;

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
        StartPos = transform.localPosition;

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
                CenterTile.GetComponent<SpriteRenderer>().enabled = true;
                RightLoopPos.GetComponent<SpriteRenderer>().enabled = true;

                LeftLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                UpwardLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                BottomLoopPos.GetComponent<SpriteRenderer>().enabled = false;

                //transform.Translate(Vector3.left * Speed);
                gameObject.transform.position = new Vector2(transform.position.x - Speed, transform.position.y);

                if (transform.localPosition.x <= -80f)
                {
                    transform.position = new Vector3(transform.position.x + 160, LeftLoopPos.position.y, 0);
                }
            }
            if (Right) // Moving right
            {

                CenterTile.GetComponent<SpriteRenderer>().enabled = true;
                LeftLoopPos.GetComponent<SpriteRenderer>().enabled = true;

                RightLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                UpwardLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                BottomLoopPos.GetComponent<SpriteRenderer>().enabled = false;

                //transform.Translate(Vector3.right * Speed);
                gameObject.transform.position = new Vector2(transform.position.x + Speed, transform.position.y);


                if (transform.localPosition.x >= 80f)
                {
                    transform.position = new Vector3(transform.position.x - 160, LeftLoopPos.position.y, 0);
                }
            }
        }
        if (Vertical)
        {
            if (Up)
            {

                CenterTile.GetComponent<SpriteRenderer>().enabled = true;
                BottomLoopPos.GetComponent<SpriteRenderer>().enabled = true;

                LeftLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                RightLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                UpwardLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                //transform.Translate(Vector3.up * Speed); // maybe just increment the value by a whole number instead? hmm
                gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + Speed);

                if (transform.localPosition.y >= 80)
                {
                    transform.position = new Vector3(UpwardLoopPos.position.x, transform.position.y - 160, 0);
                }
            }
            if (Down)
            {

                CenterTile.GetComponent<SpriteRenderer>().enabled = true;
                UpwardLoopPos.GetComponent<SpriteRenderer>().enabled = true;


                LeftLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                RightLoopPos.GetComponent<SpriteRenderer>().enabled = false;
                BottomLoopPos.GetComponent<SpriteRenderer>().enabled = false;

                //transform.Translate(Vector3.down * Speed);
                gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - Speed);


                if (transform.localPosition.y <= -80f)
                {
                    transform.position = new Vector3(BottomLoopPos.transform.position.x, transform.position.y + 160, 0);
                }
            }
        }

    }

    public void ResetAllPositions()
    {
        transform.localPosition = StartPos;
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
