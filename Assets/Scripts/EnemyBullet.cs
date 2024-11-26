using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void FixedUpdate()
    {
        Destroy(gameObject, 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the colliders
            CircleCollider2D bulletCollider = GetComponent<CircleCollider2D>();
            Collider2D enemyCollider = other.collider;

            if (bulletCollider != null && enemyCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, enemyCollider); // Ignore collision
            }
        }
        else
        {
            Destroy(gameObject); // Destroy the bullet on other collisions
        }
    }

}
