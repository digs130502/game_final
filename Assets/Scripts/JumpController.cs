using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int jumpPower;
    [SerializeField] BoxCollider2D groundCheck;
    [SerializeField] BoxCollider2D wallCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isWalled;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0;
        isWalled = Physics2D.OverlapAreaAll(wallCheck.bounds.min, wallCheck.bounds.max, groundLayer).Length > 0;
        if ((Input.GetButtonDown("Jump") && isGrounded) || (Input.GetButtonDown("Jump") && isWalled))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

    }

}
