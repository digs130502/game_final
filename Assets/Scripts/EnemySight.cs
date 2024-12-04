using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] BoxCollider2D sight;
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 15f;
    [SerializeField] EnemyPatrol enemyMovement;
    [SerializeField] AudioClip shootSound;
    private bool playerInSight = false;
    private bool isShooting = false; // New flag to prevent multiple coroutines

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 shootingDirection = enemyMovement.isFacingRight ? gun.right : -gun.right;

        rb.AddForce(shootingDirection * bulletForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = true;
            if (!isShooting) // Start the coroutine only if not already shooting
            {
                StartCoroutine(ShootRoutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    IEnumerator ShootRoutine()
    {
        isShooting = true; // Indicate that shooting is active
        while (playerInSight)
        {
            Shoot();
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            yield return new WaitForSeconds(1f); // Wait for 1 second before shooting again
        }
        isShooting = false; // Reset the flag when shooting stops
    }
}
