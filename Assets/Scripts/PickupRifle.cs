using UnityEngine;

public class PickupRifle : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject playerRifle;
    public GameObject pickupRifle;

    [Header("Rifle Assign Things")]
    public playerScript player;
    private float radius = 2.5f;

    private void Awake()
    {
        playerRifle.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                playerRifle.SetActive(true);
                pickupRifle.SetActive(false);
                // pickup sound

                // objective completeed 
            }
        }
    }

}
