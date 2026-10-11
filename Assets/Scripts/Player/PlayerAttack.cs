using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerController.IsDead || !playerController.canMove)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            TryAttack();
    }

    private void TryAttack()
    {
        if (!playerController.IsGrounded()) return;
        if (playerController.IsDefending) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) return;

        animator.SetTrigger("Attack");
    }

    public void CancelAttack()
    {
        animator.ResetTrigger("Attack");
    }
}