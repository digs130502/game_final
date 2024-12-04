using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    void Start()
    {
        // Ignore collisions between bullets
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
    }

    void FixedUpdate()
    {
        Destroy(bullet, 1f); // Destroy after 1 second
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")) return; // Ignore collisions with other bullets
        Destroy(gameObject); // Destroy on other collisions
    }
}
