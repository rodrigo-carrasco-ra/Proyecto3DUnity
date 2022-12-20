using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 90f;
    [SerializeField] float addIntensity = 1f;
    [SerializeField] AudioClip pickup;
    [SerializeField] float volume = 1.0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<FlashlightSystem>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<FlashlightSystem>().AddLightIntensity(addIntensity);
            AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position, volume);
            Destroy(gameObject);

        }
    }
}
