using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerController>()) 
        {
            if (other.gameObject.transform.parent.tag == "Player1") 
            {
                print("No");
            }
            else
            {
                print("Yes");
            }
        
        }
    }
}
