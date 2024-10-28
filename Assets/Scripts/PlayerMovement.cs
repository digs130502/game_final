using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [Range(0f, 1f)]
    [SerializeField] float groundDecay;
    [SerializeField] float groundSpeed;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    float xInput;
    public bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveWithInput();

    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();

    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0)
        {
            body.velocity *= groundDecay;
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void GetInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            body.velocity = new Vector2(xInput * groundSpeed, body.velocity.y);

            float direction = Mathf.Sign(xInput);

            //Check if the direction has changed
            if ((direction > 0 && !isFacingRight) || (direction < 0 && isFacingRight))
            {
                FlipPlayer();
            }
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight; // Toggle direction
    }
}
