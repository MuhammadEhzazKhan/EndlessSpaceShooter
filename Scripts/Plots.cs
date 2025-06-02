using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Plots : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Vector3 placementOffset;  // Add this to adjust placement position


    private GameObject tower;
    private Color startColor;
    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
    /* private void OnMouseDown() {
        if(tower != null) return;
        GameObject towerToBuild=BuildManager.main.GetSelectedTower();
        if (towerToBuild == null) return;

        // Adjust position using an offset or center the tower on the plot
        Vector3 adjustedPosition = transform.position + placementOffset; 

        Instantiate(towerToBuild, adjustedPosition, Quaternion.identity);
    
     
    } */
    private void OnMouseDown()
    {
        // Debug.Log("Mouse Down on Plot: " + gameObject.name);
        // Debug.Log("Current Tower: " + tower);

        if (tower != null)
        {
            // Debug.Log("Tower already placed.");
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > GameManager.main.currency){
            Debug.Log("not enough money");
            return;
        }
        GameManager.main.SpendCurrency(towerToBuild.cost);


        if (towerToBuild == null)
        {
            // Debug.Log("No tower selected.");
            return;
        }

        Vector3 adjustedPosition = transform.position + placementOffset;
        tower = Instantiate(towerToBuild.prefab, adjustedPosition, Quaternion.identity);
        // Debug.Log("Tower placed at: " + adjustedPosition);
    }
}