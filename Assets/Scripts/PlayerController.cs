using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;


public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float moveSpeed = 3;

    [Header("Jumping")]
    [SerializeField] private float jumpPower;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Ground Check")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.05f, 0.05f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Gravity")] 
    [SerializeField] private float baseGravity = 2;
    [SerializeField] private float maxFallSpeed = 18;
    [SerializeField] private float fallSpeedMultiplier = 2;
    
    private float horizontalMovement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (GameManager.Instance.GameStates != GameManager.GameState.Playing)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (jumpsRemaining > 0)
        {
            if (callbackContext.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (callbackContext.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    private void Gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity =
                new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, - maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }
    
    private void Update()
    {
        if (GameManager.Instance.GameStates != GameManager.GameState.Playing)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        HandleMovement();
        GroundCheck();
        Gravity();
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        horizontalMovement = callbackContext.ReadValue<Vector2>().x;
    }
    private void HandleMovement()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }
}
