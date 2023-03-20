using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentInteract : MonoBehaviour
{
    Collider[] colliders;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponents<Collider>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManagerHey.player1Interact && PlayerManagerHey.player2Interact) 
        {
            //Play Animation;
            PlayerManagerHey.player1Interact = false;
            PlayerManagerHey.player2Interact = false;

            animator.Play("GateOpen");
            Debug.Log("played animation");
            foreach (Collider col in colliders) 
            {
                col.isTrigger = true;
            }

        }
    }
}
