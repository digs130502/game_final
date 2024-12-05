using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemySight : MonoBehaviour
{
    [SerializeField] BoxCollider2D sight;
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 15f;
    [SerializeField] EnemyPatrol enemyMovement;
    [SerializeField] AudioClip shootSound;

    [SerializeField] AudioMixerGroup soundEffectsGroup; // Reference to the Sound Effects group in the Audio Mixer

    private bool playerInSight = false;
    private bool isShooting = false; // Flag to prevent multiple coroutines

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
            PlayShootingSound(shootSound); // Use the updated method for playing sound
            yield return new WaitForSeconds(1f); // Wait for 1 second before shooting again
        }
        isShooting = false; // Reset the flag when shooting stops
    }

    void PlayShootingSound(AudioClip clip)
    {
        if (clip != null)
        {
            // Create an AudioSource at runtime to use the Sound Effects group
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = soundEffectsGroup; // Assign the Sound Effects group
            audioSource.Play();

            // Destroy the AudioSource after the clip finishes playing
            Destroy(audioSource, clip.length);
        }
    }
}
