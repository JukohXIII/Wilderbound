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
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        movementDirection = new Vector3(rawInput.x, 0, rawInput.y).normalized;
        float speedPercent = movementDirection.magnitude;
        animator.SetFloat("Speed", speedPercent * moveSpeed);
    }

    void FixedUpdate()
    {
        if (movementDirection.magnitude >= 0.1f)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}


