using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    // This script is responsible for controlling the enemy's behavior, including movement, attacking, and respawning. It will also handle interactions with the player and other game elements.
    public Animator animator;
    // Velocidade de movimento
    public float moveSpeed = 2f;
    // movimento horizontal
    public Vector2 moveDirection;
    // movimento vertical
    public Vector2 verticalMoveDirection = Vector2.down;
    // ataque e dano de ataque
    public int attackDamage = 10;
    // posição de respawn do inimigo 
    public float respawnPosition;
    public float respawnyPosition;
    private bool upmoving = false;
    public bool isSpecialEnemy = false;
    public float specialEnemeySpeed = 8f;
    public Vector2 specialEnemyTarget;
    private float timer = 0f;
    public bool specialAttackUsed = false;
    public GameObject specialAttackprefab;
    private GameObject specialAttackinstance;
    private List<GameObject> specialattacks = new List<GameObject>();
    [SerializeField] private AudioClip specialAttackSound;
    //private bool canMove;
    // interação com o jogador e outros elementos do jogo
    // regra de inimigo especial para recuperar vida do jogador


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // atribui a posição de respawn do inimigo a partir da variável respawnPosition da classe EnemyRespawn
        respawnPosition = transform.position.x;
        respawnyPosition = transform.position.y;
        GameObject target = GameObject.FindWithTag("WayPoint");
        specialEnemyTarget = target.transform.position;
        if (respawnPosition <= 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (respawnyPosition >= 1)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (isSpecialEnemy == true)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (animator.GetBool("Death") == false)
        {
            if (respawnPosition >= 1 && respawnyPosition <= 0 && isSpecialEnemy == false)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, 0);
            }
            if (respawnPosition < 0 && respawnyPosition <= 0 && isSpecialEnemy == false)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, 0);

            }
            if (respawnyPosition >= 1 && isSpecialEnemy == false)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, 0);
                if (transform.position.y <= 4 && upmoving == false)
                {
                    transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, 0);
                }
                if (transform.position.y <= -3)
                {
                    upmoving = true;
                }
                if (upmoving == true)
                {
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime, 0);
                }
                if (transform.position.y >= 3 && upmoving == true)
                {
                    upmoving = false;
                }
            }
            if (isSpecialEnemy == true)
            {

                if (transform.position.y != 4 && transform.position.x != 0 && specialAttackUsed == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, specialEnemyTarget, specialEnemeySpeed * Time.deltaTime);
                    timer = 0;
                    return;
                }
                if (specialAttackUsed == false)
                {
                    SpecialEnemyAttack();
                }
                timer += Time.deltaTime;
                while (timer <= 4f)
                {
                    return;
                }

                if (transform.position.y >= -2.9 && specialAttackUsed == true)
                {
                    transform.Translate(Vector2.down * specialEnemeySpeed * 0.5f * Time.deltaTime);

                }
                if (specialAttackUsed == true && specialAttackinstance != null)
                {
                    int i = 0;
                    foreach (GameObject specialAttack in specialattacks)
                    {
                        if (i == 0)
                        {
                            specialAttack.transform.Translate(Vector2.down * 5f * Time.deltaTime);
                            Object.Destroy(specialAttack, 5f);
                        }
                        if (i == 1)
                        {
                            specialAttack.transform.Translate(Vector2.left * 4f * Time.deltaTime);
                            specialAttack.transform.Translate(Vector2.down * 4f * Time.deltaTime);
                            Object.Destroy(specialAttack, 5f);
                        }
                        if (i == 2)
                        {
                            specialAttack.transform.Translate(Vector2.right * 4f * Time.deltaTime);
                            specialAttack.transform.Translate(Vector2.down * 4f * Time.deltaTime);
                            Object.Destroy(specialAttack, 5f);
                        }
                        i++;
                    }
                }

            }
        }
        else
        {
            return;
        }

    }

    void SpecialEnemyAttack()
    {

        Debug.Log("Special Enemy Attack!");
        for (int i = 0; i < 3; i++)
        {
            specialAttackinstance = Instantiate(specialAttackprefab, new Vector2(-0.65f,3.4f), Quaternion.identity);
            specialattacks.Add(specialAttackinstance);
            

        }
        specialAttackUsed = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("SpecialCounter");
        SoundManager.instance.PlaySound(specialAttackSound, transform, 1f);


    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyObjectCollider"))
        {
            // Destroy the enemy object when it collides with the destroy object collider
            Object.Destroy(this.gameObject);

        }
    }

    void SpecialAttackMovement()
    {
        Debug.Log("Special Attack Movement!");
        
        
    }
}

