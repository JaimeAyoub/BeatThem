using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;

    public float speed = 3f;
    public float range = 5f;
    public LayerMask raycastLayerMask; 
    public float attackRange = 1.5f; 

    private EnemyAttack enemyAttack;
    public bool isPlayerInRange;

    void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            
            Debug.DrawRay(transform.position, direction * range, Color.green);

           
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, raycastLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isPlayerInRange = true;

               
                if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
                {
                    rb.velocity = direction * speed;
                }
                else
                {
                    rb.velocity = Vector2.zero; 
                    if(enemyAttack.IsAttacking() == false && enemyAttack.CanAttack())
                    {

                        StartCoroutine( enemyAttack.Attack()); 
                    }
                }
            }
            else
            {
                isPlayerInRange = false;
                rb.velocity = Vector2.zero; 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            isPlayerInRange = false;
            rb.velocity = Vector2.zero; 
        }
    }
}