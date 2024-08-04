using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera followCamera;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float fallTimer = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] TextMeshProUGUI timerText;

    private Animator animator;
    private bool isGrounded;
    private Vector3 playerVelocity;
    private float fallCounter;
    private Vector3 movementInput;

    public Vector3 gravityDirection = Vector3.down;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        fallCounter = fallTimer;
    }

    public void HandleMovement()
    {
        CheckGrounded();

        if (fallCounter <= 0)
        {
            fallCounter = 0f;
            timerText.color = Color.red;
            Time.timeScale = 0f;
        }

        if (isGrounded && Vector3.Dot(playerVelocity, gravityDirection) > 0)
        {
            playerVelocity -= Vector3.Project(playerVelocity, gravityDirection);
        }

        SetMovementByGravity();
        Vector3 movementDirection = movementInput.normalized;

        if(movementInput.magnitude == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }

        if(!isGrounded)
        {
            animator.SetBool("Falling", true);
            fallCounter -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Falling", false);
            fallCounter = fallTimer;
        }
        
        controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, -gravityDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity += -gravityDirection * Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity += -gravityDirection * gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public GravityDirection GetGravityDirection(Vector3 gravityVector)
    {
        if (gravityVector == Vector3.up)
            return GravityDirection.Up;
        else if (gravityVector == Vector3.down)
            return GravityDirection.Down;
        else if (gravityVector == Vector3.left)
            return GravityDirection.Left;
        else if (gravityVector == Vector3.right)
            return GravityDirection.Right;
        else if (gravityVector == Vector3.forward)
            return GravityDirection.Forward;
        else if (gravityVector == Vector3.back)
            return GravityDirection.Backward;

        throw new System.ArgumentException("Invalid gravity vector");
    }

    private void SetMovementByGravity()
    {
        GravityDirection gravityDir = GetGravityDirection(gravityDirection);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        switch (gravityDir)
        {
            case GravityDirection.Up:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
                break;
            case GravityDirection.Down:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
                break;
            case GravityDirection.Left:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, verticalInput, 0);
                break;
            case GravityDirection.Right:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, verticalInput, 0);
                break;
            case GravityDirection.Forward:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, verticalInput, 0);
                break;
            case GravityDirection.Backward:
                movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, verticalInput, 0);
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }
}

public enum GravityDirection
{
    Up,
    Down,
    Left,
    Right,
    Forward,
    Backward
}


