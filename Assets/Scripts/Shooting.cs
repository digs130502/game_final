using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AudioClip shootingAudio;

    [SerializeField] SpriteRenderer playerSpriteRenderer; // Reference to the player's sprite renderer
    [SerializeField] Sprite ramboSprite; // Rambo's sprite
    [SerializeField] Sprite bruceSprite; // Bruce's sprite
    [SerializeField] Sprite chuckSprite; // Chuck's sprite

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
            AudioSource.PlayClipAtPoint(shootingAudio, transform.position);
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
            AudioSource.PlayClipAtPoint(shootingAudio, transform.position);
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
            AudioSource.PlayClipAtPoint(shootingAudio, transform.position);
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
}
