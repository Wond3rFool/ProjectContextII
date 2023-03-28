using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //input fields
    //private PlayerActionAsset playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

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
    private Canvas canvas;

    private TextMeshProUGUI text;
    private string[] lines;
    private float textSpeed = 0.09f;

    private int index;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputAsset = GetComponent<PlayerInput>().actions;
        animator = GetComponent<Animator>();
        canvas = gameObject.transform.parent.GetComponentInChildren<Canvas>();
        player = inputAsset.FindActionMap("Player");


        //playerActionsAsset = new PlayerActionAsset();
        //animator = this.GetComponent<Animator>();
    }

    private void Start() 
    {
        canvas.gameObject.SetActive(false);
        text = canvas.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        //playerActionsAsset.Player.Jump.started += DoJump;
        //playerActionsAsset.Player.Attack.started += DoAttack;
        //move = playerActionsAsset.Player.Movement;
        //playerActionsAsset.Player.Enable();
        player.FindAction("Jump").started += DoJump;
        player.FindAction("Interact").started += NpcInteract;
        player.FindAction("Environment").started += EnvironmentInteract;
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
        player.FindAction("Environment").started -= EnvironmentInteract;
        player.Disable();
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        if (forceDirection != Vector3.zero)
        {
            animator.Play("PlayerWalk");
        }
        else
        {
            animator.Play("PlayerIdle");
        }

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
        if (canvas.gameObject.activeInHierarchy)
        {
            if (text.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
        else 
        {
            foreach (Collider collider in colliders)
            {
                Debug.Log(collider);
                if (collider.TryGetComponent(out NpcInteractable npc))
                {
                    npc.Interact(canvas);
                }
            }
        }

    }
    public void FillArray(int length, string[] _lines) 
    {
        lines = new string[length];
        lines = _lines;
        index = 0;
        StartDialogue();
    }

    private void EnvironmentInteract(InputAction.CallbackContext obj) 
    {
        float interactRange = 5f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out EnvironmentInteract npc))
            {
                Debug.Log(gameObject.transform.parent.tag);
                if (gameObject.transform.parent.tag == "Player1")
                {
                    PlayerManagerHey.player1Interact = true;
                    npc.nearMe1 = true;
                }
                else if (gameObject.transform.parent.tag == "Player2") 
                {
                    PlayerManagerHey.player2Interact = true;
                    npc.nearMe2 = true;
                }
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        text.text= string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
    private bool IsGrounded()
    {
        Debug.Log("hello");
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.51f)) 
            return true;
        else
            return false;
    }
}
