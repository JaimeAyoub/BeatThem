using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator anim;
    public GameObject AttackHitBox;

    public float AttackCD = 2f;
    public float AttackDuration = 0.5f;

    private float _lastAttackTime;
    private bool _isAttacking;

    void Start()
    {
        AttackHitBox.SetActive(false);
        _lastAttackTime = -AttackCD;
    }

    // Corrutina para realizar el ataque
    public IEnumerator Attack()
    {
        if (CanAttack())
        {
            _isAttacking = true;


            anim.SetBool("isAttacking", true);


            AttackHitBox.SetActive(true);


            yield return new WaitForSeconds(AttackDuration);


            AttackHitBox.SetActive(false);


            anim.SetBool("isAttacking", false);


            _lastAttackTime = Time.time;

            _isAttacking = false;
        }
    }


    public bool CanAttack()
    {
        return (Time.time - _lastAttackTime) >= AttackCD;
    }


    public bool IsAttacking()
    {
        return _isAttacking;
    }
}