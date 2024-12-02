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
            StartCoroutine(ShootRoutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
            StopCoroutine(ShootRoutine()); // Stop shooting when player exits sight
        }
    }

    IEnumerator ShootRoutine()
    {
        while (playerInSight)
        {
            Shoot();
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            yield return new WaitForSeconds(1f); // Wait for 1 second before shooting again
        }
    }

}
