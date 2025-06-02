using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float bps = 1f;
    private Transform target;
    private float timeUntilFire;

    private void Update() {
        // Check if there is no target or if the target is out of range
        if (target == null || Vector2.Distance(transform.position, target.position) > targetingRange) {
            FindTarget();
        } else {
            RotateTowardsTarget();
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire > 1f/bps){
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }
    private void Shoot(){
        GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletobj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget(){
        // Cast a circle around the turret to find all enemies within range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // If enemies are found, find the closest one
        if (hits.Length > 0){
            Transform closestTarget = null;
            float shortestDistance = Mathf.Infinity;

            foreach (var hit in hits){
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < shortestDistance){
                    shortestDistance = distanceToEnemy;
                    closestTarget = hit.transform;
                }
            }
            target = closestTarget; // Set the closest enemy as the target
        }
    }

    private void RotateTowardsTarget(){
        // Calculate the angle between the turret and the target
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
         angle -= 90f;
        
        // Rotate the turret towards the target
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        // Draw the targeting range as a wire disc for debugging purposes
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
