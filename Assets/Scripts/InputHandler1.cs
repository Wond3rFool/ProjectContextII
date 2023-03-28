using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputHandler1 : MonoBehaviour
{
    private PlayerInput playerInput;

    public float duration = 5.0f;  // Duration of the timer in seconds
    public Text countdownText;     // Reference to the UI text element to display the countdown

    private float remainingTime;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }

    public void DisableInput()
    {
        playerInput.DeactivateInput();
    }

    void Update()
    {
    }
}
