using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    [Header("Player Controls")]
    public InputActionAsset playerControls;
    InputAction moveAction;
    InputAction sprintAction;
    InputAction jumpAction;

    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprintSpeed = 3f;

    [Header("Player Character controller and Gravity")]
    public CharacterController cController;
    public float gravity = -9.81f;

    [Header("Player Animator")]
    public Animator playerAnimator;

    [Header("Player Script Cameras")]
    public Transform playerCamera;

    [Header("Player Jumping and Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;

    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if (onSurface && velocity.y <0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cController.Move(velocity * Time.deltaTime);

        PlayerMove();
        Jump();
    }

    private void OnEnable()
    {
        var map = playerControls.FindActionMap("Player");

        moveAction = map.FindAction("Move");
        sprintAction = map.FindAction("Sprint");
        jumpAction = map.FindAction("Jump");

        moveAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
    }

    void PlayerMove()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        float speed = sprintAction.IsPressed() ? playerSprintSpeed : playerSpeed;

        if (direction.magnitude >= 0.1f)
        {
            playerAnimator.SetBool("Idle", false);
            if (sprintAction.IsPressed())
            {
                playerAnimator.SetBool("Running", true);
                playerAnimator.SetBool("Walk", false);
            }
            else
            {
                playerAnimator.SetBool("Running", false);
                playerAnimator.SetBool("Walk", true);
            }
            playerAnimator.SetBool("FireWalk", false);
            playerAnimator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            cController.Move(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            playerAnimator.SetBool("Idle", true);
            playerAnimator.SetBool("Walk", false);
            playerAnimator.SetBool("Running", false);
        }
    }

    void Jump()
    {
        if(jumpAction.triggered && onSurface)
        {
            playerAnimator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2f * gravity);
        }
    }
}
