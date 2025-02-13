using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator anim;

    public float TakeDamageTimer;
    public float TakeDamageCD;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        HP_Enemy _hpEnemy = GetComponent<HP_Enemy>();
        if (collision.CompareTag("PlayerAttack"))
        {
            if (Time.time - TakeDamageTimer >= TakeDamageCD)
            {
                _hpEnemy.TakeDamage(1);
                TakeDamageTimer = Time.time;
            }
        }
    }

  

}
