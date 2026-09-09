using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerController : MonoBehaviour
{
    public float playerVelocity = 10.0f;
    public float jumpVelocity = 5.0f;
    public bool isGrounded = true;
    private float horizontalInput;
    private Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    [SerializeField] private AudioClip footStepSound;
    [SerializeField] private AudioClip jumpSound;
    

    [HideInInspector]
    public bool isFacingLeft;

    public bool spawnFacingLeft;
    private Vector2 facingLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
        initializtion();
    
    }
        void initializtion()
    {
        // Define a escala para o lado esquerdo
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        // Verifica se o jogador deve nascer virado para a esquerda e ajusta a escala
        if (spawnFacingLeft)
        {
            transform.localScale = facingLeft;
            isFacingLeft = true;
        }
      
    }

    void Flip()
    {
        // Inverte a escala do jogador para virar a direção
        if (isFacingLeft) 
        { 
            transform.localScale = facingLeft;
        }    
        if (!isFacingLeft) 
        {
            // Cria uma nova escala invertida para o lado direito
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0 && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
        }
        else if (horizontalInput < 0 && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Move o jogador horizontalmente com base na entrada do teclado
        transform.Translate(new Vector2(Input.GetAxis("Horizontal"), 0) * playerVelocity * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        //Implementar som de passos do jogador futuramente.
        /*if (horizontalInput != 0 && isGrounded)
        {
            SoundManager.instance.PlaySound(footStepSound, transform, 1f);
        }*/


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();            
        }

    }

    void Jump()
    {
        if (!isGrounded) 
        {
            return; // Prevent jumping if not grounded
        }
        animator.SetTrigger("Jump");
        SoundManager.instance.PlaySound(jumpSound, transform, 1f);
        rb.linearVelocity = new Vector2(0, jumpVelocity);
        isGrounded = false;
        animator.SetBool("isGrounded", false);

    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f && rb.linearVelocity.y <= 0)
                {
                    isGrounded = true;
                    animator.SetBool("isGrounded", true);
                }

            }

        }

    }


}

