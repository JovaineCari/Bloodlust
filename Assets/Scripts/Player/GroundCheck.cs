using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Collider2D col2d;
    public bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()   
    {
        
    }
}
