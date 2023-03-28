using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentInteract : MonoBehaviour
{
    TextMeshPro text;
    Animator animator;

    private float interactRange = 18f;

    public bool nearMe1 = false;
    public bool nearMe2 = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponentInChildren<TextMeshPro>();
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManagerHey.player1Interact && PlayerManagerHey.player2Interact && nearMe1 && nearMe2) 
        {
            //Play Animation;
            PlayerManagerHey.player1Interact = false;
            PlayerManagerHey.player2Interact = false;
            nearMe1 = false;
            nearMe2 = false;

            animator.Play("GateOpen");
            Debug.Log("played animation");
        }

        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in collidersInRange)
        {
            if (collider.TryGetComponent(out PlayerController player))
            {
                text.gameObject.SetActive(true);
            }
            else 
            {
                text.gameObject.SetActive(false);
            }

        }
    }
}
