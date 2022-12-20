using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 40f;
    AudioSource hitSound;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
        hitSound = GetComponent<AudioSource>();
    }

    public void OnDamageTaken()
    {
        Debug.Log(name + " recieved damage");
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        hitSound.Play();
        target.GetComponent<PlayerHealth>().TakeDamage(damage);
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
