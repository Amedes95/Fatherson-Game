using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    public List<Transform> MoveLocations; // manually add the move-to locations
    public float MovementSpeed;

    Transform TargetPosition;

    public bool Reversible;

    PauseMenu PauseScreen;


    private void Awake()
    {
        PauseScreen = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<PauseMenu>();
        transform.position = MoveLocations[0].position; // start the blade at the first node
        TargetPosition = MoveLocations[0]; // first target is the first in the list
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition.position, MovementSpeed); // constantly move the blade to the target position
            if (transform.position == TargetPosition.position) // once target is reached, choose a new target
            {
                ChooseTarget();
            }
        }

    }

    public void ChooseTarget()
    {
        for (int i = 0; i < MoveLocations.Count; i++) // iterate through the list of possible targets
        {
            if (transform.position == MoveLocations[i].position) // Once we find the node that we are currently positioned at, move to the next in line
            {
                if (i+1 == MoveLocations.Count) // quick check to see if its the last node in the list
                {
                    if (Reversible == false) // If it's not reversible, then just teleport it back to node 0
                    {
                        transform.position = MoveLocations[0].position;
                        TargetPosition = MoveLocations[0];
                    }
                    else // but if it is reversible...
                    {
                        // reverse the list
                        MoveLocations.Reverse();
                        i = 0; // reset indexer
                    }

                }
                else
                {
                    TargetPosition = MoveLocations[i + 1]; // set targt node to the next node in the list
                }
            }
        }
    }
}
