using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerManagerHey : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;
    [SerializeField]
    private Canvas canvasToAssign;

    private PlayerInputManager playerInputManager;

    public static bool player1Interact = false;
    public static bool player2Interact = false;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }
    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }
    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        Canvas canvasToPlace = Instantiate(canvasToAssign);
        //need to use the parent due to the structure of the prefab
        Transform playerParent = player.transform.parent;
        playerParent.gameObject.tag = "Player" + players.Count;
        playerParent.position = startingPoints[players.Count - 1].position;

        //convert layer mask (bit) to an integer 
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        //set the layer
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        //add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        //set the action in the custom cinemachine Input Handler
        canvasToPlace.worldCamera = playerParent.GetComponentInChildren<Camera>();
        canvasToPlace.planeDistance = 1f;

        playerParent.GetComponentInChildren<CinemachineInputHandler>().look = player.actions.FindAction("Look");

    }
}
