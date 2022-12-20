using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;

    [SerializeField] AudioClip pickup;
    [SerializeField] float volume = 0.5f;
    [SerializeField] AmmoType ammoType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }

    }
}
