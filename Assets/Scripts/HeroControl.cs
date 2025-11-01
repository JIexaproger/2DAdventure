using System;
using UnityEngine;

public class HeroControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputSystem inputSystem;


    public float moveSpeed;
    private Vector2 moveDirection;


    private void Awake()
    {
        inputSystem = new InputSystem();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        inputSystem.Enable();

        inputSystem.Player.Attack.performed += Attack;
    }
    private void OnDisable()
    {
        inputSystem.Disable();

        inputSystem.Player.Attack.performed -= Attack;
    }
    
    private void Update()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        Vector2 input = inputSystem.Player.Move.ReadValue<Vector2>();
        input.Normalize();

        rb.AddForce(moveDirection * moveSpeed);
    }


    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
    }
}
