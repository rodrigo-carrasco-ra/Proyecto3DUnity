using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] float healthKit = 50f;
    public string playerName;
    public string playerHealth;


    private void Awake()
    {
      //  playerName = NameTransfer.instance.playerName;
    }
    //funcion public que reduce hitpoints dependiendo del daño del zombie
    void Update()
    {
        DisplayHitPoints();
        if (hitPoints > 100f)
        {
            hitPoints = 100f;
        }
    }

    public void DisplayHitPoints()
    {
        hpText.text ="Puntos de vida de "+playerName+" : " + hitPoints.ToString();
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
