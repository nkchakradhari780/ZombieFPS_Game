using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class playerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;

    [Header("Player Character controller and Gravity")]
    public CharacterController cController;

    [Header("Player Jumping and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalAxis, 0, verticalAxis).normalized;   

        if(moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cController.Move(moveDirection * playerSpeed * Time.deltaTime);
        }
    }
}
