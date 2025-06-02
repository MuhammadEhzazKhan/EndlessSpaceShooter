using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform startPoint;
    public Transform[] path;
    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;

    }

    public void IncCurrency(int ammount)
    {
        currency += ammount;
    }

    public bool SpendCurrency(int ammount)
    {
        if (ammount <= currency)
        {
            currency -= ammount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money to buy");
            return false;
        }
    }
}
