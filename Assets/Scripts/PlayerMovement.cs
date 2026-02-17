using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;

	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float jumpForce = 5.0f;
	private Rigidbody2D rb;
	private Vector2 direction;
	private bool isJumping = false;

	private void OnEnable()
	{
		inputActions.FindActionMap("Player").Enable();
	}

	private void Awake()
	{
        rb = GetComponent<Rigidbody2D>();
		moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");
	}

    void Update()
    {
		direction = moveAction.ReadValue<Vector2>();
		if (jumpAction.WasPressedThisFrame())
			isJumping = true;
	}

	private void FixedUpdate()
	{
		if (direction.x != 0)
			rb.linearVelocityX = direction.x * speed;
		else
			rb.linearVelocityX = 0;

		if (isJumping)
		{
			rb.AddForceY(jumpForce, ForceMode2D.Impulse);
			isJumping = false;
		}
	}
}
