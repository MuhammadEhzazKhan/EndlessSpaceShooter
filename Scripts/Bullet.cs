using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float destroyTime = 10f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

     private void Start() {
        // Automatically destroy the bullet after `destroyTime` seconds
        Destroy(gameObject, destroyTime);
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
        //bullet rotation towards enemy
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, destroyTime);
        // Debug.Log("Collided with: " + other.gameObject.name + " on Layer: " + LayerMask.LayerToName(other.gameObject.layer));
        if (other.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}