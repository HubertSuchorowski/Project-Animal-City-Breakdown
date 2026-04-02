using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class movement : MonoBehaviour
{
    
    [SerializeField] public float playerSpeed;
    [SerializeField] public float playerjump;
    private float gravity = -9.81f;
    private bool grounded;

    public CharacterController controller;

    public GameObject camera;
    private Vector3 playerVelocity;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Update(){
        Move();
        Jump();
    }

    void Move()
    {
        Vector2 Input = moveAction.action.ReadValue<Vector2>();
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * Input.y + right * Input.x;        
        move = Vector3.ClampMagnitude(move, 1f);

        playerVelocity.y += gravity * Time.deltaTime;
        Vector3 finalMove = move * playerSpeed +  Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);    
    }

    void Jump(){
        grounded = controller.isGrounded;
        
        if (grounded)
        {
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }
        
         if (grounded && jumpAction.action.WasPressedThisFrame())
        {
            playerVelocity.y = Mathf.Sqrt(playerjump * -2f * gravity);
        }

    }
}
