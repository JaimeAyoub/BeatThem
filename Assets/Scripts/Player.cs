using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class Player : MonoBehaviour
{
    [Header("Componentes del player")]
    public Rigidbody2D rb;
    public GameObject AttackHitBox;
    public Animator anim;
    public SpriteRenderer sprite;
    public PostProcessVolume postProcessVolume;
    public Vignette vignette;
    public float vignetteIntensity;

    //public Slider healthSlider;
    public Color damageColor;

    public float PushForce;
    public float TakeDamageTimer;
    public float TakeDamageCD;

    [Header("Cosas para el flip")]
    public bool isFacingRight = true;
    [Header("Variables de movimiento")]
    public float Xspeed = 0f;
    public float Yspeed = 0f;
    public float inputX = 0f;
    public float inputY = 0f;
    [Header("Variables para el ataque")]
    public float AttackTimer = 0f;
    public bool isAttacking = false;
    public float AttackCD = 0.5f;

    [Header("Variables Tweens")]
    public int effectLoop;
    public float damageTweenTime;



    //Controles: Movimiento con WASD, pegas con K.
    void Start()
    {

        CheckComponents();
        AttackHitBox.SetActive(false);
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f; 
        }
    }

    
    void Update()
    {
        Move();
        Attack();
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
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K) && (Time.time - AttackTimer) >= AttackCD)
        {
            anim.SetBool("IsAttack", true);
            AttackHitBox.SetActive(true);
            AttackTimer = Time.time;
            isAttacking = true;
        }
        if (AttackHitBox.activeSelf && (Time.time - AttackTimer) >= AttackCD)
        {
            AttackHitBox.SetActive(false);
            anim.SetBool("IsAttack", false);
            isAttacking = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("EnemyAttack"):
                if(Time.time - TakeDamageTimer >= TakeDamageCD)
                {
                    GetDamage();
                    TakeDamageTimer = Time.time;
                }
                break;
        }
    }

    private void FlipController()
    {
        if (inputX > 0 && !isFacingRight && !isAttacking )
            Flip();
        else if (inputX < 0 && isFacingRight && !isAttacking)
            Flip();
    }
    private void Flip()
    {
       
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void GetDamage()
    {
        sprite.DOColor(damageColor, (damageTweenTime/effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
        DOTween.To(() => vignette.intensity.value, x=>vignette.intensity.value = x, vignetteIntensity,0.5f).SetId("VignetteTween").OnComplete(() =>
        {
            
            DOTween.To(() => vignette.intensity.value,
                       x => vignette.intensity.value = x,
                       0f,
                       0.5f)
                   .SetId("VignetteTween");
        });
        KnockBack();
      
      
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
                Debug.LogError("No se enconotro Animator!");
        }
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
            if (sprite == null)
                Debug.LogError("No se enconotro sprite!");
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                Debug.LogError("No se enconotro RigidBody2D!");
        }
    }
   
    

}
