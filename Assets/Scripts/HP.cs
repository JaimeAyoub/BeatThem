using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HP : MonoBehaviour
{
    public int HpMax;
    public int currentHp;

    protected virtual void Start()
    {
        SetHealth();
    }


    public virtual void TakeDamage(int damage)
    {
        if (currentHp > 0)
        {
            currentHp -= damage;
        }

        if (currentHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        StopAllCoroutines();
        DOTween.KillAll();
     
        Destroy(gameObject);
    }

    public void SetHealth()
    {
        currentHp = HpMax;
    }
}