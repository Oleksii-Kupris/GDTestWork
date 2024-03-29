using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
    public float Hp;
    public float Damage;
    public float AtackSpeed;
    public float AttackRange = 2;


    public Animator AnimatorController;
    public NavMeshAgent Agent;

    public EnemyType enemyType;
    public GameObject smallGoblin;

    private float lastAttackTime = 0;
    private bool isDead = false;

    private void Start()
    {
        SceneManager.Instance.AddEnemie(this);
        Agent.SetDestination(SceneManager.Instance.Player.transform.position);
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
            Agent.isStopped = true;
            return;
        }

        var distance = Vector3.Distance(transform.position, SceneManager.Instance.Player.transform.position);

        if (distance <= AttackRange)
        {
            Agent.isStopped = true;
            if (Time.time - lastAttackTime > AtackSpeed)
            {
                lastAttackTime = Time.time;
                SceneManager.Instance.Player.ResiveHP(-Damage);
                AnimatorController.SetTrigger("Attack");
            }
        }
        else
        {
            Agent.isStopped = false;
            Agent.SetDestination(SceneManager.Instance.Player.transform.position);
        }
        AnimatorController.SetFloat("Speed", Agent.speed);

    }
    private void Die()
    {
        SceneManager.Instance.RemoveEnemie(this);
        isDead = true;
        AnimatorController.SetTrigger("Die");

        if (enemyType == EnemyType.GoblinBig)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(smallGoblin, transform.position, Quaternion.identity);
            }
        }
    }
    public void Hit()   //Animation event
    {
    }
    public void EndHit() //Animation event
    {
    }

}
