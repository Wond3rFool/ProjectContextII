using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerInteraction : MonoBehaviour
{
    public static bool player1HasInteracted = false;
    public static bool player2HasInteracted = false;



    private void Update()
    {
        if (player1HasInteracted && player2HasInteracted) 
        {
            WorldInteraction();
        }
    }

    private void WorldInteraction() 
    {
        //open gate
        //turn off fog

    }
}
