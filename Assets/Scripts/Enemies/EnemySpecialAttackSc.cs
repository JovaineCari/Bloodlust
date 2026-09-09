using UnityEngine;

public class EnemySpecialAttackSc : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private AudioClip specialAttackSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("EnemySpecialAttackSc: Player collided with enemy special attack.");
            // Play the special attack sound
            SoundManager.instance.PlaySound(specialAttackSound, transform, 1f);
            // Trigger the special attack animation
            animator.SetTrigger("Hit");
            Object.Destroy(this.gameObject, 0.5f); // Destroy the special attack after 0.5 seconds)
        }
    }
}
