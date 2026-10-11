using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance;
    public int attackDamage = 25;

    private Animator animator;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        else
            Destroy(gameObject);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerController.Instance.IsDead || !PlayerController.Instance.canMove)
            return;

        if (Input.GetKeyDown(KeyCode.F))
            TryAttack();
    }

    private void TryAttack()
    {
        if (!PlayerController.Instance.IsGrounded()) return;
        if (PlayerController.Instance.IsDefending) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) return;

        animator.SetTrigger("Attack");
    }

    public void CancelAttack()
    {
        animator.ResetTrigger("Attack");
    }
}