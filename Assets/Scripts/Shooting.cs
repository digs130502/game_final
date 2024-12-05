using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] AudioClip defaultShootingAudio; // Default sound for Rambo and Bruce
    [SerializeField] AudioClip chuckShootingAudio;   // Unique sound for Chuck

    [SerializeField] SpriteRenderer playerSpriteRenderer; // Reference to the player's sprite renderer
    [SerializeField] Sprite ramboSprite; // Rambo's sprite
    [SerializeField] Sprite bruceSprite; // Bruce's sprite
    [SerializeField] Sprite chuckSprite; // Chuck's sprite

    [SerializeField] AudioMixerGroup soundEffectsGroup; // Reference to the Sound Effects group in the Audio Mixer

    private bool isFiringContinuously = false; // To manage continuous fire for Bruce

    void Update()
    {
        // Determine the character's shooting behavior based on the sprite
        if (playerSpriteRenderer.sprite == ramboSprite)
        {
            HandleRamboShooting();
        }
        else if (playerSpriteRenderer.sprite == bruceSprite)
        {
            HandleBruceShooting();
        }
        else if (playerSpriteRenderer.sprite == chuckSprite)
        {
            HandleChuckShooting();
        }
    }

    void HandleRamboShooting()
    {
        if (Input.GetKeyDown("j"))
        {
            ShootSingleBullet();
            PlayShootingSound(defaultShootingAudio);
        }
    }

    void HandleBruceShooting()
    {
        if (Input.GetKey("j") && !isFiringContinuously) // Hold to shoot continuously
        {
            isFiringContinuously = true;
            StartCoroutine(ContinuousFire());
        }
        else if (Input.GetKeyUp("j"))
        {
            isFiringContinuously = false;
        }
    }

    void HandleChuckShooting()
    {
        if (Input.GetKeyDown("j"))
        {
            ShootShotgun();
            PlayShootingSound(chuckShootingAudio);
        }
    }

    void ShootSingleBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 shootingDirection = playerMovement.isFacingRight ? gun.right : -gun.right;
        rb.AddForce(shootingDirection * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator ContinuousFire()
    {
        while (isFiringContinuously)
        {
            ShootSingleBullet();
            PlayShootingSound(defaultShootingAudio);
            yield return new WaitForSeconds(0.2f); // Adjust fire rate as needed
        }
    }

    void ShootShotgun()
    {
        // Fire three bullets at slightly different angles
        float spreadAngle = 15f; // Angle difference between bullets

        for (int i = -1; i <= 1; i++) // Loop to create three bullets
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Adjust the shooting direction for the spread
            float angle = i * spreadAngle;
            Quaternion rotation = Quaternion.Euler(0, 0, angle) * gun.rotation;

            Vector2 shootingDirection = rotation * Vector2.right;
            if (!playerMovement.isFacingRight) shootingDirection = -shootingDirection;

            rb.AddForce(shootingDirection * bulletForce, ForceMode2D.Impulse);
        }
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
