using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public Vector2 distanceEnemyToPlayer;
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
        FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            // FollowPlayer();
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

    public void FollowPlayer()
    {
        if (player != null)
        {
             distanceEnemyToPlayer = (player.transform.position - transform.position).normalized;


            Debug.DrawRay(transform.position, distanceEnemyToPlayer * range, Color.green);
            
          
            RaycastHit2D hit = Physics2D.Raycast(transform.position, distanceEnemyToPlayer, range, raycastLayerMask);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                isPlayerInRange = true;


                if (Vector2.Distance(transform.position, player.transform.position) > attackRange)
                {
                    rb.velocity = distanceEnemyToPlayer * speed;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    if (enemyAttack.IsAttacking() == false && enemyAttack.CanAttack())
                    {

                        StartCoroutine(enemyAttack.Attack());
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

    public Vector2 returnDistanceEnemyPlayer()
    {
        return distanceEnemyToPlayer;
    }



}