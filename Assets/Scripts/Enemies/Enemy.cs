using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public int score;
    private bool isDead = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    
    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;
        //play hurt animation

        if (currentHealth <= 0)
        {
            isDead = true;
            ScoreCounter.Instance.IncreaseScore(score);
            Die();
            
        }
    }
    
    void Die()
    {
        
        //play death animation
        animator.SetBool("Death", true);
        //stop enemy movement
        transform.Translate(Vector2.zero);
        //disable enemy
        Object.Destroy(this.gameObject, 0.5f);
        
    }

}
