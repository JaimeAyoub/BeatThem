using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    [Header("adasdasd")]
    //public Slider healthSlider;
    public Color damageColor;



    public int effectLoop;
    public float damageTweenTime;
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
        switch (collision.tag)
        {
            case ("PlayerAttack"):
                if (Time.time - TakeDamageTimer >= TakeDamageCD)
                {
                    TakeDamage();
                    TakeDamageTimer = Time.time;
                }
                break;
            default:
                break;
        }
    }

    private void TakeDamage()
    {

        sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
     
    }
    
}
