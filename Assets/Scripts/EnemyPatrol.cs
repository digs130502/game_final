using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float speed;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public bool isFacingRight = true;
    [SerializeField] private PolygonCollider2D polygonCollider;
    [SerializeField] private BoxCollider2D collisionBoxCollider;

    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioMixerGroup soundEffectsGroup; // Reference to the Sound Effects group in the Audio Mixer

    private AudioSource audioSource; // AudioSource to play sounds

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;

        // Initialize and configure the AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = soundEffectsGroup;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        isFacingRight = !isFacingRight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 1f);
        Gizmos.DrawWireSphere(pointB.transform.position, 1f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            PlayShootSound();
            Destroy(gameObject, 0.1f); // Delay slightly to allow sound to play
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Collider2D otherCollider = other.collider; // Get the collider of the other enemy
            if (collisionBoxCollider != null && otherCollider != null)
            {
                Physics2D.IgnoreCollision(collisionBoxCollider, otherCollider);
            }
            if (polygonCollider != null && otherCollider != null)
            {
                Physics2D.IgnoreCollision(polygonCollider, otherCollider);
            }
        }
    }

    private void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
