using System.Collections.Generic;
using Unity;
using UnityEditor;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    private HashSet<GameObject> triggeredObjects = new HashSet<GameObject>();
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        if (triggeredObjects.Contains(other.gameObject))
        {
            // The trigger has already been triggered
            return;
        }

        // The trigger has not yet been triggered by either player
        triggeredObjects.Add(other.gameObject);
        PlayerManagerHey.score += score;

        // Do something with the new shared score (e.g. update a UI element)
        Debug.Log("Shared score: " + PlayerManagerHey.score);
    }
}