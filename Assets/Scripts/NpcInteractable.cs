using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{
    private float interactRange;

    public string textToWrite;

    public string[] lines;

    private void Awake()
    {
        interactRange = 6f;
    }
    public void Interact(Canvas canvas) 
    {
        //ChatBubble.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), textToWrite);
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out PlayerController player)) 
            {
                canvas.gameObject.SetActive(true);
                player.FillArray(lines.Length, lines);
                //player.GetComponentInChildren<Dialogue>().lines = lines;
            }
        }

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
