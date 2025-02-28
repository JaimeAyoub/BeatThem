using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public HP_Enemy _hpEnemy;
    public EnemyPatrol _enemyPatrol;
    public Animator anim;
    public bool isFacingRight = false;

    public float TakeDamageTimer;
    public float TakeDamageCD;
    void Start()
    {
        _hpEnemy = GetComponent<HP_Enemy>();
       
    }

    // Update is called once per frame
    void Update()
    {
        FlipController();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
       
        if (collision.CompareTag("PlayerAttack"))
        {
            if (Time.time - TakeDamageTimer >= TakeDamageCD)
            {

                TakeDamage();
                
               

            }
        }
    }

    void TakeDamage()
    {
        _hpEnemy.TakeDamage(1);
        TakeDamageTimer = Time.time;
    }
    private void FlipController()
    {
        if ( _enemyPatrol.returnDistanceEnemyPlayer().x < 0 && !isFacingRight)
            Flip();
        else if (_enemyPatrol.returnDistanceEnemyPlayer().x > 0 && isFacingRight)
            Flip();
    }
    private void Flip()
    {

        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


}
