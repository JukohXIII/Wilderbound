using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Look")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 60f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform; // glisse Main Camera ici
    [SerializeField] private Vector3 cameraShoulderOffset = new Vector3(0.5f, 1.6f, 0f); // X = épaule, Y = hauteur
    [SerializeField] private float cameraDistance = 3f;
    [SerializeField] private bool invertY = false;

    private Rigidbody rb;
    private Animator animator;
    private Vector2 rawInput;
    private Vector2 lookInput;
    private Vector3 movementDirection;

    private float yaw;   // rotation horizontale — pilote le corps ET la caméra
    private float pitch; // rotation verticale — caméra uniquement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.sleepThreshold = 0;
        Cursor.lockState = CursorLockMode.Locked;

        yaw = transform.eulerAngles.y;
    }

    void OnMove(InputValue input) => rawInput = input.Get<Vector2>();
    void OnLook(InputValue input) => lookInput = input.Get<Vector2>();

    void Update()
    {
        // Accumulate look rotation from raw mouse input — single source of truth
        yaw += lookInput.x * mouseSensitivity;
        pitch += (invertY ? -1f : 1f) * lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Player body rotates horizontally only
        rb.rotation = Quaternion.Euler(0f, yaw, 0f);

        float inputMagnitude = Mathf.Clamp01(rawInput.magnitude);
        movementDirection = (transform.forward * rawInput.y + transform.right * rawInput.x).normalized;

        animator.SetFloat("Speed", inputMagnitude * moveSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("MotionSpeed", inputMagnitude);
    }

    void FixedUpdate()
    {
        if (movementDirection.magnitude >= 0.1f)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Quaternion lookRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Shoulder pivot: au-dessus du joueur, décalé sur le côté selon le yaw
        Vector3 pivotPosition = transform.position
            + Vector3.up * cameraShoulderOffset.y
            + (Quaternion.Euler(0f, yaw, 0f) * Vector3.right) * cameraShoulderOffset.x;

        Vector3 desiredCameraPosition = pivotPosition - (lookRotation * Vector3.forward) * cameraDistance;

        cameraTransform.position = desiredCameraPosition;
        cameraTransform.rotation = lookRotation;
    }

    void OnFootstep(AnimationEvent animationEvent) { }
    void OnLande(AnimationEvent animationEvent) { }
}