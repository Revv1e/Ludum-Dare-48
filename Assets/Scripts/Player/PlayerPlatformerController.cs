using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameManager gameManager;

    [HideInInspector]
    public Vector2 move;
    public bool isAlive = true;

    public Transform lastTouchedOrb;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    { 
    }

    private void Update()
    {
        ComputeVelocity();
        if (transform.position.y <= -5)
        {
            isAlive = false;
        }

        if (!isAlive)
        {
            gameManager.hasDiedOnce = true;
            gravityModifier = 0.0f;
            rb2d.gravityScale = 0.0f;
            rb2d.bodyType = RigidbodyType2D.Static;
            transform.position = Vector3.Slerp(transform.position, lastTouchedOrb.position, Time.deltaTime * 3.0f);
            if (Mathf.Approximately(transform.position.z, lastTouchedOrb.position.z))
            {
                gravityModifier = 0.25f;
                rb2d.gravityScale = 1.0f;
                rb2d.bodyType = RigidbodyType2D.Dynamic;
                isAlive = true;
            }
        }
    }
    protected override void ComputeVelocity()
    {
        move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = spriteRenderer.flipX;
        if (move.x < 0)
        {
            flipSprite = true;
        }
        else if (move.x > 0)
        {
            flipSprite = false;
        }
        spriteRenderer.flipX = flipSprite;

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}