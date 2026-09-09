using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public Transform specialAttackPoint;
    public GameObject specialAttackPrefab;
    private GameObject specialAttackInstance;
    public Transform[] specialAttacks;
    private List<GameObject> activeSpecialAttacks = new List<GameObject>();
    public int maxHealth = 100;
    public int currentHealth;
    public float attackRange = 0.5f;
    public Vector2 specialAttackRange = new Vector2(1f, 1f);
    public LayerMask enemyLayers;
    public LayerMask specialEnemy;
    public Animator animator;
    public int attackDamage = 40;
    public int specialAttackDamage = 60;
    public bool specialCounter = false;
    public GameObject specialCounterUI;
    public float seconds;
    public bool isDead = false;
    public bool invulnerability = false;
    public WaitForSeconds wait;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color shineColor = Color.red;
    public float shineDuration = 0.1f;
    public float totalDuration;
    float nextAttackTime = 0f;
    public float attackRate = 2f;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip specialAttackSound;
    [SerializeField] private AudioClip reliefSound;
    public PlayerHealth healthBar;
    public GameManager GameManager;
    public GameObject player;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        totalDuration = 5f * (Time.deltaTime + 0.1f); 
    }

    public void StartInvulnerability()
    {
        if (invulnerability == false) 
        {
            return; // If not invulnerable, do nothing
        }
        StartCoroutine(ShineRoutine());
    }

    private IEnumerator ShineRoutine()
    {
        float timer = 0f;
        while (timer < totalDuration)
        {
            spriteRenderer.color = shineColor;
            yield return new WaitForSeconds(shineDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(shineDuration);
            timer += shineDuration;
        }
        spriteRenderer.color = originalColor;
        invulnerability = false; // Reset invulnerability after the duration
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpecialAttack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelfDamage(20);

        }

        //mecanic for self-damage every 4 seconds, but it is commented out for dificulty reasons, but it can be used for a hardcore mode or something like that
        /*seconds += Time.deltaTime;

        if (seconds >= 4)
        {
            SelfDamage(5);
            // flash the player sprite to indicate self-damage
            // play a sound effect to indicate self-damage Heavy breathing sound effect
            seconds = 0;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpecialCounter"))
        {
            specialCounter = true;
            Debug.Log("Special attack counter activated!");
            GameObject.Destroy(other.gameObject);
        }
    }

    void Attack()
    {
        Debug.Log("Player attacks!");
        // Play an attack animation
        animator.SetTrigger("Attack");
        //move attack point to the right if facing right and to the left if facing left
        
        // Play attack sound
        SoundManager.instance.PlaySound(attackSound, transform, 1f);
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            SoundManager.instance.PlaySound(hitSound, transform, 1f);
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            if (enemy.GetComponent<Enemy>().transform.position.x < transform.position.x)
            {
                enemy.GetComponent<Enemy>().transform.Translate(Vector2.left * 0.5f);
            }
            else
            {
                enemy.GetComponent<Enemy>().transform.Translate(Vector2.right * 0.5f);
            }
            

        }
        Collider2D[] specialEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, specialEnemy);
        foreach (Collider2D enemy in specialEnemies)
        {
            Debug.Log("You drained " + enemy.name + "You can use your special attack!");
            currentHealth += 30;
            healthBar.SetHealth(currentHealth);
            SoundManager.instance.PlaySound(reliefSound, transform, 1f);
            specialCounter = true;
            specialCounterUI.GetComponent<RawImage>().color = Color.white;
            enemy.GetComponent<Enemy>().TakeDamage(100);

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if (specialAttackPoint == null)
            return;
        Gizmos.DrawWireCube(specialAttackPoint.position, specialAttackRange);
    }

    void SpecialAttack()
    {
        if (specialCounter == false)
            return;
        Debug.Log("Player performs a special attack!");
        specialCounter = false;
        //change the color of the special counter UI to grey
        specialCounterUI.GetComponent<RawImage>().color = Color.black;
        for (int i = 0; i<specialAttacks.Length; i++)
        {
            specialAttackInstance = Instantiate(specialAttackPrefab, specialAttacks[i].position, specialAttacks[i].rotation);
            activeSpecialAttacks.Add(specialAttackInstance);
        }
        
        // Implement special attack logic here
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(specialAttackPoint.position, specialAttackRange, 0f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(specialAttackDamage);
            SoundManager.instance.PlaySound(specialAttackSound, transform, 1f);
        }

        foreach(GameObject specialAttackclone in activeSpecialAttacks)
        {
            Object.Destroy(specialAttackclone, 0.5f); // Destroy the special attack after 1 second
        }

    }

    void SelfDamage(int damage)
    {
        Debug.Log("Player takes " + damage + " self-damage!");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        animator.SetTrigger("Death");
        this.gameObject.SetActive(false);
        GameManager.GameOver();

                
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (invulnerability)
        {
            return; // Ignore damage if invulnerable
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hurt");
            SelfDamage(10);
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.right * 1f); // Push the player to the left
                
            }
            else
            {
                transform.Translate(Vector2.left * 1f); // Push the player to the right
            }
            invulnerability = true;
            StartInvulnerability();
        }
        if (collision.gameObject.CompareTag("SpecialEnemy"))
        {
            animator.SetTrigger("Hurt");
            SelfDamage(10);
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.right * 1f); // Push the player to the left
            }
            else
            {
                transform.Translate(Vector2.left * 1f); // Push the player to the right
            }
            invulnerability = true;
            StartInvulnerability();

        }
        if (collision.gameObject.CompareTag("EnemySpecialAttack"))
        {
            animator.SetTrigger("Hurt");
            SelfDamage(20);
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                transform.Translate(Vector2.right * 1f); // Push the player to the left
            }
            else
            {
                transform.Translate(Vector2.left * 1f); // Push the player to the right
            }
            invulnerability = true;
            StartInvulnerability();

        }

    }
}

