using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    AudioSource ammoPickSound;
    [SerializeField] AmmoType ammoType;

    private void Start()
    {
        ammoPickSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            ammoPickSound.Play();
            Destroy(gameObject);
        }

    }
}
