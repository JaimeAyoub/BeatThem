using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float AttackTimer = 0f;
    public bool isAttacking = false;
    public float AttackCD = 0.5f;

    public GameObject AttackHitBox;
    public Animator anim;

    void Start()
    {
        AttackHitBox.gameObject.SetActive(false);
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            if (anim == null)
                Debug.LogError("No se enconotro Animator!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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
}