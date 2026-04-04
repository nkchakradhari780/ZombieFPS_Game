using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupRifle : MonoBehaviour
{
    [Header("Player controls")]
    public InputActionAsset playerControls;
    InputAction fireAction;
    InputAction interactAction; 

    [Header("Rifle's")]
    public GameObject playerRifle;
    public GameObject pickupRifle;
    public playerPunch playerPunch;

    [Header("Rifle Assign Things")]
    public playerScript player;
    private float radius = 2.5f;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f; 


    private void Awake()
    {
        playerRifle.SetActive(false);
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            Debug.LogError("playerControls is NOT assigned!");
            return;
        }

        var map = playerControls.FindActionMap("Player");

        if (map == null)
        {
            Debug.LogError("Player Action Map NOT found!");
            return;
        }

        fireAction = map.FindAction("Attack");
        interactAction = map.FindAction("Interact");

        if (fireAction == null)
        {
            Debug.LogError("Fire action NOT found!");
            return;
        }

        if (interactAction == null)
        {
            Debug.LogError("Interact action NOT found!");
            return;
        }

        fireAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
    }

    private void Update()
    {
        if(fireAction.IsInProgress() && Time.time >= nextTimeToPunch)
        {
            nextTimeToPunch = Time.time + 1f / punchCharge;

            playerPunch.Punch();

        }

        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (interactAction.triggered)
            {
                playerRifle.SetActive(true);
                pickupRifle.SetActive(false);
                // pickup sound

                // objective completeed 
            }
        }
    }

}
