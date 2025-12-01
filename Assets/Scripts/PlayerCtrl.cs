using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour {

    private Vector2 movement;
    private Rigidbody2D rb;
    public float speed = 5f;
    // Preserve older prefab field if present; helpful if your prefab serialized `movSpeed` previously
    public float movSpeed = 0f;
    // Optional: assign the Input Actions asset used in the scene (e.g. `PlyrInputs` or `InputSystem_Actions`).
    public InputActionAsset actionsAsset;
    // Try these action names in order when binding at runtime.
    public string[] actionNames = new string[] { "Move", "Movements" };

    private InputAction activeAction;
    // Animator support (parameters match the Animator screenshot)
    public Animator animator;
    public RuntimeAnimatorController animatorController;
    public string paramInputX = "InputX";
    public string paramInputY = "InputY";
    public string paramLastInX = "LastInX";
    public string paramLastInY = "LastInY";
    public string paramIsMoving = "isMoving";
    public string paramBlend = "Blend";
    // internal last non-zero input direction
    private Vector2 lastInput = Vector2.zero;
    public float inputThreshold = 0.01f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        // allow movSpeed from prefab to override new `speed` field
        if (movSpeed > 0f && Mathf.Approximately(speed, 5f)) {
            speed = movSpeed;
        }

        if (animator == null) animator = GetComponent<Animator>();
        if (animator == null && animatorController != null) {
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            Debug.Log("PlayerCtrl: Added Animator and assigned controller at runtime.");
        }
    }

    // Send-Messages callbacks (if a PlayerInput is set to "Send Messages")
    private void OnMove(InputValue value) {
        movement = value.Get<Vector2>();
        Debug.Log("OnMove (SendMessage) -> " + movement);
    }

    private void OnMovements(InputValue value) {
        movement = value.Get<Vector2>();
        Debug.Log("OnMovements (SendMessage) -> " + movement);
    }

    private void OnEnable() {
        // If an InputActionAsset is assigned, try to bind to a Vector2 action automatically.
        if (actionsAsset != null) {
            foreach (var map in actionsAsset.actionMaps) {
                foreach (var name in actionNames) {
                    var a = map.FindAction(name, true);
                    if (a != null) {
                        activeAction = a;
                        activeAction.performed += OnActionPerformed;
                        activeAction.canceled += OnActionCanceled;
                        activeAction.Enable();
                        Debug.Log($"PlayerCtrl: bound to action '{activeAction.name}' in map '{map.name}'");
                        return;
                    }
                }
            }
        }
    }

    private void OnDisable() {
        if (activeAction != null) {
            activeAction.performed -= OnActionPerformed;
            activeAction.canceled -= OnActionCanceled;
            activeAction.Disable();
            activeAction = null;
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx) {
        movement = ctx.ReadValue<Vector2>();
        // small debug to confirm callbacks are firing
        Debug.Log("OnActionPerformed -> " + movement);
    }

    private void OnActionCanceled(InputAction.CallbackContext ctx) {
        movement = Vector2.zero;
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        // Update animator parameters to match controller's expected inputs
        if (animator != null) {
            // Input parameters reflect current movement input (-1..1)
            animator.SetFloat(paramInputX, movement.x);
            animator.SetFloat(paramInputY, movement.y);

            // Blend can be used to control movement blend amount (0..1)
            float blendVal = Mathf.Clamp01(movement.magnitude);
            animator.SetFloat(paramBlend, blendVal);

            // isMoving flag
            bool moving = movement.sqrMagnitude > inputThreshold * inputThreshold;
            animator.SetBool(paramIsMoving, moving);

            // Update LastIn when input appears
            if (moving) {
                // store normalized last input to keep direction even after stopping
                lastInput = movement.normalized;
                animator.SetFloat(paramLastInX, lastInput.x);
                animator.SetFloat(paramLastInY, lastInput.y);
            }
        }
    }
}