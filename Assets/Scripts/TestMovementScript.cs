using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovementScript : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private Camera playerCamera;

    private Transform cameraTransform;

    private Vector3 forceDirection = Vector3.zero;


    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction interact;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        inputAsset = GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        //playerActionsAsset = new PlayerActionAsset();
        //animator = this.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //playerActionsAsset.Player.Jump.started += DoJump;
        //playerActionsAsset.Player.Attack.started += DoAttack;
        //move = playerActionsAsset.Player.Movement;
        //playerActionsAsset.Player.Enable();
        //player.FindAction("Jump").started += DoJump;
        player.FindAction("Interact").started += NpcInteract;
        move = player.FindAction("Movement");
        player.Enable();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = playerCamera.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        Vector2 moveInput = move.ReadValue<Vector2>();

        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);

        movement = movement.x * cameraTransform.right.normalized + movement.z * cameraTransform.forward.normalized; 

        movement.y = 0f;
        controller.Move(movement * Time.deltaTime * playerSpeed);
        
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (moveInput != Vector2.zero) 
        {
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }



    private void NpcInteract(InputAction.CallbackContext obj)
    {
        float interactRange = 5f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider);
            if (collider.TryGetComponent(out NpcInteractable npc))
            {
                npc.Interact();
            }
        }
    }
}
