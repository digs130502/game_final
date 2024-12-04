using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float explosionDelay = 3f;
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionForce = 500f;
    [SerializeField] LayerMask damageLayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", explosionDelay);
    }

    void Explode()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageLayer);

        foreach (Collider2D obj in hitObjects)
        {

            Debug.Log($"{obj.name} hit by grenade");

            if (obj.CompareTag("Enemy"))
            {
                Destroy(obj.gameObject);
            }
            else if (obj.CompareTag("Player"))
            {
                Health playerHealth = obj.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
