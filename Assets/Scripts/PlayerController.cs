using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
     [Header("Settings")]
    float moveSpeed = 5f;
    float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator animator;
    private Vector2 rawInput;
    private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.sleepThreshold = 0; // Set a low sleep threshold to prevent unwanted sleeping
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
        float speedPercent = rawInput.magnitude;
        
        if(cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        movementDirection = (transform.forward * rawInput.y + transform.right * rawInput.x).normalized;

        animator.SetFloat("Speed", speedPercent * moveSpeed);
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
}


