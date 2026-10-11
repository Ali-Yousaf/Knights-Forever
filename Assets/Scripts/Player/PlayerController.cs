using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float runMultiplier = 1.8f;
    [SerializeField] private GameObject shieldObject;

    public bool canMove = true;

    private float moveInput;
    private bool jumpPressed;
    private bool isDead;
    private bool isRunning;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerAttack playerAttack;
    private Coroutine shieldCoroutine;

    public bool IsDead => isDead;
    public bool IsBusy => animator.GetCurrentAnimatorStateInfo(0).IsTag("Busy");
    public bool IsDefending => Input.GetKey(KeyCode.Q) && IsGrounded();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        shieldObject.SetActive(false);
    }

    void Update()
    {
        if (isDead || !canMove) return;

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

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void Hurt()
    {
        if (isDead) return;

        playerAttack.CancelAttack();
        animator.SetTrigger("Hurt");
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canMove = false;
        moveInput = 0f;
        jumpPressed = false;
        rb.linearVelocity = Vector2.zero;

        playerAttack.CancelAttack();
        animator.SetTrigger("Death");
    }


    public void ActivateShield(float duration)
    {
        if (shieldCoroutine != null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine = StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        shieldObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        shieldObject.SetActive(false);
        shieldCoroutine = null;
    }
}