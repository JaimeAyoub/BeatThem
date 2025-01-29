using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
   
    public Rigidbody2D rb;
    public GameObject AttackHitBox;
    public Animator anim;

    public float Xspeed = 0f;
    public float Yspeed = 0f;

    public float AttackTimer = 0f;

    public float AttackCD = 0.5f;

    void Start()
    {
        AttackHitBox.SetActive(false);

    }

    
    void Update()
    {
        Move();
        Attack();   
    }

    void Move()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Xspeed, Input.GetAxisRaw("Vertical") * Yspeed);
        
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetBool("IsAttack", true);
            AttackHitBox.SetActive(true);
            AttackTimer = Time.time;
        }
        if (AttackHitBox.activeSelf && (Time.time - AttackTimer) >= AttackCD)
        {
            AttackHitBox.SetActive(false);
            anim.SetBool("IsAttack", false);
        }

    }
}
