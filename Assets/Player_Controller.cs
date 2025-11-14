using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Animator animator;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(x, 0, z).normalized;

        if (input.magnitude >= 0.1f)
        {
            // Optional: face movement direction
            transform.forward = input;

            // Optional: camera-relative movement
            if (cameraTransform != null)
            {
                Vector3 camForward = cameraTransform.forward;
                camForward.y = 0;
                camForward.Normalize();

                Vector3 camRight = cameraTransform.right;
                camRight.y = 0;
                camRight.Normalize();

                moveDirection = camForward * z + camRight * x;
            }
            else
            {
                moveDirection = input;
            }

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // Animation (optional)
        if (animator != null)
        {
            animator.SetFloat("Speed", input.magnitude);
        }
    }
}