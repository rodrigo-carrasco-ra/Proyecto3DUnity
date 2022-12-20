using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickup;
    [SerializeField] float volume = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().RestoreHealthKit();
            AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }

    }
}
