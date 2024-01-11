using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Action<float> onAttack;
    public Action<float> onSupperAttack;
    public Action<float> onHPResived;
    public Animator playerAnimation;
    public Player player;

    public float attackDamage;
    public float supperAttackDamage;
    public float cooldawnAttack;
    public float cooldawnSuperAttack;
    public float attackRange = 1;

    private bool isEndHit = true;

    public void Update()
    {
        CheckEnemyRenge();
    }
    public void Attack()
    {
        if (!isEndHit)
            return;

        isEndHit = false;
        onAttack?.Invoke(cooldawnAttack);
        playerAnimation.SetTrigger("Attack");
    }
    public void SupperAttack()
    {
        onSupperAttack?.Invoke(cooldawnSuperAttack);
        playerAnimation.SetTrigger("SupperAttack");
    }
    public void Hit()
    {
       CheckEnemyHit(attackDamage);
    }
    public void EndHit()
    {
        isEndHit = true;
    }
    public void SupperHit()
    {
        CheckEnemyHit(supperAttackDamage);
        player.ChengeToDefaultState();
    }
    private void CheckEnemyHit(float damage)
    {
        var enemies = SceneManager.Instance.Enemies;
        Enemie closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }

        if (closestEnemie != null)
        {
            var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
            if (distance <= attackRange)
            {
                closestEnemie.Hp -= damage;
                onHPResived?.Invoke(damage);
            }
        }
    }

    private void CheckEnemyRenge()
    {
        var enemies = SceneManager.Instance.Enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            if (distance <= attackRange)
            {
                SceneManager.Instance.UIController.ActiveSupperAtack();
               
            }
        }
    }
}

