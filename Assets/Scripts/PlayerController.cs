using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //input fields
    //private PlayerActionAsset playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction interact;

    //movement fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        player.FindAction("Jump").started += DoJump;
        player.FindAction("Interact").started += NpcInteract;
        player.FindAction("InteractEnviremont").started += EnviremontInteract;
        move = player.FindAction("Movement");
        player.Enable();
    }

    private void OnDisable()
    {
        //playerActionsAsset.Player.Jump.started -= DoJump;
        //playerActionsAsset.Player.Attack.started -= DoAttack;
        //playerActionsAsset.Player.Disable();
        player.FindAction("Jump").started -= DoJump;
        player.FindAction("Interact").started -= NpcInteract;
        player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private void NpcInteract(InputAction.CallbackContext obj) 
    {
        float interactRange = 5f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders) 
        {
            //Debug.Log(collider);
            if (collider.TryGetComponent(out NpcInteractable npc)) 
            {
                npc.Interact();
            }
        }
    }

    private void EnviremontInteract(InputAction.CallbackContext obj) 
    {
        float environmentRange = 5f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, environmentRange);

        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent(out EnvironmentInteract env)) 
            {
                if (gameObject.transform.parent.tag == "Player1")
                {
                    PlayerManagerHey.player1Interact = true;
                    Debug.Log("palyer 1 test");
                }

                else if (gameObject.transform.parent.tag == "Player2") 
                {
                    PlayerManagerHey.player2Interact = true;
                    Debug.Log("palyer 2 test");
                } 
                else 
                {
                    Debug.Log("No tags assigned");
                }
            }
        }
    
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }
}
