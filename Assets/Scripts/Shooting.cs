using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Check the player's facing direction and apply force accordingly
        Vector2 shootingDirection = playerMovement.isFacingRight ? gun.right : -gun.right;

        rb.AddForce(shootingDirection * bulletForce, ForceMode2D.Impulse);
    }
}
