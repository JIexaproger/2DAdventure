using UnityEngine;

public class HeroControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField]
    private Transform spriteTransform;
    private InputSystem inputSystem;


    public float moveSpeed, moveDamping;
    private Vector2 moveDirection;

    public bool isMoveDampingEnable;


    private void Awake()
    {
        inputSystem = new InputSystem();
        animator = gameObject.GetComponent<Animator>();
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
    
    private void FixedUpdate()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        Vector2 input = inputSystem.Player.Move.ReadValue<Vector2>();
        input.Normalize();
        input *= moveSpeed;

        if (input.x < 0)
        {
            spriteTransform.rotation = Quaternion.Euler(spriteTransform.rotation.x, 180, spriteTransform.rotation.z);
        }
        else if (input.x > 0)
        {
            spriteTransform.rotation = Quaternion.Euler(spriteTransform.rotation.x, 0, spriteTransform.rotation.z);
        }

        // if (input.magnitude > 0) - не сбрасывается скорость когда 0
        // {

        if (isMoveDampingEnable)
        {
            moveDirection += input;
            moveDirection *= moveDamping;
        }
        else
        {
            moveDirection = input;
        }

        animator.SetFloat("Speed", input.magnitude);
        rb.linearVelocity = moveDirection;

        // }
        
    }


    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
    }
}
