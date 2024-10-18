using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveVector;
    private Vector3 velocity;  
    private bool isGrounded;
    private float currentSpeed;
    private float smoothVelocity; 
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.1f; 
    [SerializeField] private float speed = 5.0f;
    [SerializeField] public float gravity = -19.6f;
    [SerializeField] public float jumpForce = 2.0f;
    [SerializeField] public float sprintMultiplier = 1.4f;
    [SerializeField] private float airControlFactor = 0.5f; 

    void Start() {
        controller = GetComponent<CharacterController>();
        currentSpeed = speed;
    }

    void Update() {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;  
        }

        velocity.y += gravity * Time.deltaTime;
    }

    public void Move(Vector2 input) {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        if (isGrounded) {
            Vector3 targetVelocity = moveDirection * currentSpeed;
            moveVector = Vector3.Lerp(moveVector, targetVelocity, moveSmoothTime);
        } else {
            // gimped air control
            Vector3 targetVelocity = new Vector3(moveDirection.x, 0, moveDirection.z) * currentSpeed * airControlFactor;

            if (targetVelocity.z < 0) {
                targetVelocity.z *= 0.8f;
            } 

            moveVector.x = Mathf.Lerp(moveVector.x, targetVelocity.x, moveSmoothTime);
            moveVector.z = Mathf.Lerp(moveVector.z, targetVelocity.z, moveSmoothTime);
        }

        controller.Move(transform.TransformDirection(moveVector) * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump() {
        if (isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
        }
    }

    public void Sprint(bool isSprinting) {
        if (isSprinting) {
            currentSpeed = speed * sprintMultiplier;
        } else {
            currentSpeed = speed;
        }
    }
}