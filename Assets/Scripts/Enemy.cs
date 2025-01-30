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
    public Slider healthSlider;
    public Color damageColor;

    public float life = 10;
    public float maxHealth;


    public int effectLoop;
    public float damageTweenTime;
    public float TakeDamageTimer;
    public float TakeDamageCD;
    void Start()
    {

        life = maxHealth;
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = life;
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
    private void SetHealth()
    {
        healthSlider.value = life;
    }
    private void TakeDamage()
    {
        if(life>0)
            life--;
        sprite.DOColor(damageColor, (damageTweenTime / effectLoop)).SetLoops(effectLoop, LoopType.Yoyo);
        SetHealth();
    }
    
}
