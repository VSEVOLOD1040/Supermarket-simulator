using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float verticalRotation = 0f;
    public float maxVerticalAngle = 80f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public bool isGrounded;

    public Rigidbody rb;
    public bool mouselookenabled;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Awake()
    {
        EventBus.MouseLock += MouseLock;
        EventBus.CameraLookEnabled += SwitchCameraRotation;

    }

    void MouseLock(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
    void SwitchCameraRotation(bool state)
    {
        if (state)
        {
            mouselookenabled = true;
        }
        else
        {
            mouselookenabled = false;
        }

    }
    void Update()
    {

        if (mouselookenabled)
        {
            HandleMouseLook();
        }

        HandleJump();
    }

    void FixedUpdate()
    {

        HandleMovement();

    }

    void HandleMouseLook()
    {


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = move * moveSpeed;
        Vector3 currentVelocity = rb.velocity;

        rb.velocity = new Vector3(velocity.x, currentVelocity.y, velocity.z);
    }

    void HandleJump()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void TurnOnCharacterMouseController(bool state)
    {
        MouseLock(state);
        SwitchCameraRotation(state);
    }
}