using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{
    private float interactRange;

    public string textToWrite;

    public string[] lines;

    private TextMeshPro text;

    private bool interactable = false;

    private void Awake()
    {
        interactRange = 16f;
        text = GetComponentInChildren<TextMeshPro>();
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
                interactable = true;
                //player.GetComponentInChildren<Dialogue>().lines = lines;
            }
        }

    }

    public void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out PlayerController player) && !interactable)
            {
                transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z / 2));
                text.gameObject.SetActive(true);
            }
            else 
            {
                text.gameObject.SetActive(false);
            }

        }
    }
}
