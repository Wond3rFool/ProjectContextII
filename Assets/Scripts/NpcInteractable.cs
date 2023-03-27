using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{
    private float interactRange;

    public string textToWrite;

    private void Awake()
    {
        interactRange = 6f;
    }
    public void Interact() 
    {
       ChatBubble.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), textToWrite);
    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider);
            if (collider.TryGetComponent(out PlayerController player))
            {
                transform.LookAt(player.transform.position);
                
            }

        }
    }
}
