using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float throwArcHeight = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        Vector2 throwDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Vector2 force = new Vector2(throwDirection.x * throwForce, throwArcHeight);
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
