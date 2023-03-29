using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject) 
        {
            SceneSwitcher.SwitchToEnd(PlayerManagerHey.score);
        }
    }
}
