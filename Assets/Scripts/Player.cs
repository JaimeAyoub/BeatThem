using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("Componentes del player")] public Rigidbody2D rb;
    public GameObject AttackHitBox;
    public Animator anim;

    private PlayerAttack _playerAttack;


    public bool isFigthing = false;

    public float PushForce;
    public float TakeDamageTimer;
    public float TakeDamageCD;

    [Header("Cosas para el flip")] public bool isFacingRight = true;
    [Header("Variables de movimiento")] public float Xspeed = 0f;
    public float Yspeed = 0f;
    public float inputX = 0f;
    public float inputY = 0f;


    //Controles: Movimiento con WASD, pegas con K.
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        CheckComponents();
    }


    void Update()
    {
        Move();
        FlipController();
        //Debug.Log(inputX);
    }

    void Move()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(inputX * Xspeed, inputY * Yspeed);

        if (inputX == 0 && inputY == 0)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HP_Player _hpPlayer = GetComponent<HP_Player>();
        switch (collision.tag)
        {
            case ("EnemyAttack"):
                if (Time.time - TakeDamageTimer >= TakeDamageCD)
                {
                    _hpPlayer.TakeDamage(1);
                    TakeDamageTimer = Time.time;
                }

                break;
        }
    }

    private void FlipController()
    {
        if (inputX > 0 && !isFacingRight && !_playerAttack.isAttacking)
            Flip();
        else if (inputX < 0 && isFacingRight && !_playerAttack.isAttacking)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void KnockBack()
    {
        if (inputX > 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(-PushForce, rb.velocity.y));
        }
        else if (inputX < 0)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(PushForce, rb.velocity.y));
        }
    }


    private void CheckComponents()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            if (anim == null)
                Debug.LogError("No se encontro Animator!");
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("No se encontro RigidBody2D!");
        }
    }
}