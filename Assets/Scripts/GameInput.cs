using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    public event EventHandler OnJumpPerformed; 
    
    public static GameInput Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        
        playerInputActions = new PlayerInputActions();
        
        playerInputActions.Player.Enable();
        
        playerInputActions.Player.Jump.performed += JumpPerformed;
    }

   
    private void OnDestroy()
    {
        playerInputActions.Player.Jump.performed -= JumpPerformed;
        
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputMovement = playerInputActions.Player.Movement.ReadValue<Vector2>();

        return inputMovement;
    }
    
    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        OnJumpPerformed?.Invoke(this, EventArgs.Empty);
    }
}
