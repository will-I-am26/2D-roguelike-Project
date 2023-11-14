using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombatScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
   
    public int attackDamage = 20;
    public float attackRange = 0.5f;
    float nextAttackTime = 0f;
    public float attackRate = 2f;
    int startingHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    void attack()
    {
        // play the animation 
        animator.SetTrigger("attack");

        // detect enemy in range 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }

        //apply damaage 
    }

    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
