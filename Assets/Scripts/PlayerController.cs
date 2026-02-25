using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
     [Header("Settings")]
    float moveSpeed = 10f;
    float rotationSpeed = 50f;

    private Rigidbody rb;
    private Animator animator;
    private Vector2 rawInput;
    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.sleepThreshold = 0; // Set a low sleep threshold to prevent unwanted sleeping
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnMove(InputValue input)
    {
        rawInput = input.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        // vitesse globale à l'unique paramètre de l'animator "speed"
        // A modifier plus tard quand on aura des animations de pas chassé et reculons
        float inputMagnitude = Mathf.Clamp01(rawInput.magnitude);
        float speedPercent = inputMagnitude; // * moveSpeed; // You can multiply by moveSpeed if you want to use the actual speed value in the animator
        
        if(cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            // Smooth camera movement
            // rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
            // Direct camera movement
            rb.rotation = targetRotation;
        }

        movementDirection = (transform.forward * rawInput.y + transform.right * rawInput.x).normalized;

        animator.SetFloat("Speed", speedPercent * moveSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("MotionSpeed", inputMagnitude);
        //animator.SetFloat("Horizontal", rawInput.x, 0.1f, Time.deltaTime);
        //animator.SetFloat("Vertical", rawInput.y, 0.1f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        if(movementDirection.magnitude >= 0.1f)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnFootstep(AnimationEvent animationEvent)
    {
        // Play footstep sound here
        // You can use animationEvent to determine which foot is stepping and play different sounds if needed
    }

    void OnLande(AnimationEvent animationEvent)
    {
        // Play landing sound here
    }
}


