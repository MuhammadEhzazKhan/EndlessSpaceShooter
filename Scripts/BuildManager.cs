using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("Refrences")]
    // [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;
    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTurret(int _selectedTower){
        selectedTower = _selectedTower;
    }
}