using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMe : MonoBehaviour
{
    public GameObject Pivot;
    public bool Flipped;
    public Transform A;
    public Transform B;


    private void FixedUpdate()
    {
        Raycasting();
        if (Flipped)
        {
            Debug.Log("Flip");
            if (Pivot.GetComponent<Turret>().Increasing)
            {
                Pivot.GetComponent<Turret>().Increasing = false;
            }
            else
            {
                Pivot.GetComponent<Turret>().Increasing = true;
            }
        }
    }

    public void Raycasting() // Checks to see if there are walls between the enemy and player
    {
        Debug.DrawLine(A.position, B.position, Color.green);
        Flipped = Physics2D.Linecast(A.position, B.position, 1 << LayerMask.NameToLayer("Ground"));
    }
}
