using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float positionSpeedMultiplier = 7f;

    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerWidth = 0.7f;

   
    
    [Header("Jumping")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpMultiplier = 3;
    
    private Rigidbody2D rigidbody2D;
    private bool isGrounded;
    private Vector2 vecGravity;

    private bool isJumping = false;
    private float jumpCounter = 0;
    
    
    
    public static PlayerController Instance { get; private set; }
    
    private bool isWalking = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnJumpPerformed += HandleJumping;

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnDestroy()
    {
        if (GameInput.Instance == null) return; 
        GameInput.Instance.OnJumpPerformed -= HandleJumping;
    }

    private void HandleJumping(object sender, EventArgs e)
    {
        /*isJumping = false;
        jumpCounter = 0;

        if (rigidbody2D.velocity.y > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.6f);
        }*/
        if (CheckIsGrounded())
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            isJumping = true;
            jumpCounter = 0;
        }
    }

    private void Update()
    {
        HandleMovement();
        if (rigidbody2D.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
            }

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rigidbody2D.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }
    }

    private bool CheckIsGrounded()
    {
        return isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.8f, 0.3f),
            CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    private void HandleMovement()
    {
        Vector2 inputMovement = GameInput.Instance.GetMovementVector();
        
        Vector3 movementDirection = new Vector3(inputMovement.x, 0, 0);

        var moveDistance = positionSpeedMultiplier * Time.deltaTime;

        var canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, 
            playerWidth, movementDirection, moveDistance);
        
        isWalking = movementDirection != Vector3.zero;

        if (!canMove)
        {
            var moveDirX = new Vector3(movementDirection.x, 0, 0).normalized;
            canMove = movementDirection.x is < -.5f or > 0.5f && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, 
                playerWidth, moveDirX, moveDistance);

            if (canMove)
            {
                movementDirection = moveDirX;
            }
        }
        if(canMove)
        {
            transform.position += movementDirection * moveDistance;
        }
    }
    
}
