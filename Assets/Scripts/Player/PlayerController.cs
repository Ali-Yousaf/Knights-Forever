using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runMultiplier = 1.8f;

    public bool canMove = true;
    private float moveInput;
    private bool jumpPressed;
    private bool isDead;
    private bool isRunning;
    private Rigidbody2D rb;
    private Animator animator;

    bool IsBusy => animator.GetCurrentAnimatorStateInfo(0).IsTag("Busy");

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;
        if (canMove == false) return;

        bool grounded = IsGrounded();
        bool defending = Input.GetKey(KeyCode.Q) && grounded;

        animator.SetBool("IsGrounded", grounded);
        animator.SetBool("IsDefending", defending);

        moveInput = IsBusy ? 0f : Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float target = Mathf.Abs(moveInput) * (isRunning ? 1f : 0.5f);
        animator.SetFloat("Speed", target, 0.1f, Time.deltaTime);

        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        if (Input.GetKeyDown(KeyCode.F) && grounded && !defending &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            animator.SetTrigger("Attack");

        if (Input.GetKeyDown(KeyCode.Space) && !IsBusy)
            jumpPressed = true;
    }

    void FixedUpdate()
    {
        float speed = moveSpeed * (isRunning ? runMultiplier : 1f);
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (jumpPressed && IsGrounded())
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        jumpPressed = false;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void Hurt()
    {
        if (isDead) return;

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Hurt");
    }
    
    public void Die()
    {
        isDead = true;
        canMove = false;
        moveInput = 0f;
        rb.linearVelocity = Vector2.zero;
        
        animator.SetTrigger("Death");
    }
}