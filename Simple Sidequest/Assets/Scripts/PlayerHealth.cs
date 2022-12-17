using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float healthKit = 50f;

    //funcion public que reduce hitpoints dependiendo del daño del zombie
    void Update()
    {
        DisplayHitPoints();
        if (hitPoints > 100f)
        {
            hitPoints = 100f;
        }
    }

    private void DisplayHitPoints()
    {
        hpText.text = "Puntos de vida: " + hitPoints.ToString();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    public void RestoreHealthKit()
    {
        hitPoints += healthKit;
    }


}
