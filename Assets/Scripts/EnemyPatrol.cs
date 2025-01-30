using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public float speed;

    private float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.gameObject; ;
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

            if(distance < 1)
            {
                rb.transform.position = Vector2.MoveTowards(rb.transform.position, player.transform.position, speed * Time.deltaTime);
                
            }
        }
    }
}
