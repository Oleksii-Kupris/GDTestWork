using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHP;
    public float Hp;
    public float MovementSpeed = 5;
    public float RotationSpeed = 5;
    private bool isDead = false;
    private Rigidbody _rb;
    public Animator AnimatorController;
    public PlayerAttack playerAttack;

    #region States
    public StateMachine movementSM;
    public MovingState standingState;
    public AttackState attackState;
    public SupperAttackState supperAttackState;
    #endregion

    private void OnEnable()
    {
        playerAttack.onHPResived += ResiveHP;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        movementSM = new StateMachine();
        standingState = new MovingState(this, movementSM);
        attackState = new AttackState(this, movementSM);
        movementSM.Initialize(standingState);
    }
    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }

        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    public void Move(float speed, float rotationSpeed)
    {
        Vector3 targetVelocity = speed * transform.forward * Time.deltaTime;
        targetVelocity.y = _rb.velocity.y;
        _rb.velocity = targetVelocity;

        _rb.angularVelocity = rotationSpeed * Vector3.up * Time.deltaTime;
        AnimatorController.SetFloat("Speed", speed);
    }
    public void Attack()
    {
        playerAttack.Attack();
    }
    public void SupperAttack()
    {
        playerAttack.SupperAttack();
    }
    public void ChengeToDefaultState()
    {
        movementSM.ChangeState(standingState);
    }
    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");

        SceneManager.Instance.GameOver();
    }
    public void ResetParameters()
    {
        _rb.velocity = Vector3.zero;
        AnimatorController.SetFloat("Speed", 0);
    }
    public void ResiveHP(float hp)
    {
        Hp += hp;
        if (Hp > MaxHP)
        {
            Hp = MaxHP;
        }   
        SceneManager.Instance.UIController.HPCounter(Hp, MaxHP);
    }


}
