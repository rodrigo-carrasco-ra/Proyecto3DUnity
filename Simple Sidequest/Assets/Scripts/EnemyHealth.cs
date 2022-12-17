using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth: MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    bool isDead = false;
    float countTime = 5f;
    //funcion public que reduce hitpoints dependiendo del daño del arma

    public void TakeDamage(float damage)
    {
        GetComponent<EnemyAI>().OnDamageTaken();
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator Death()
    {

        if (hitPoints<=0 && !isDead)
        isDead = true;
        GetComponent<Animator>().SetTrigger("death");
        yield return new WaitForSeconds(countTime);
        Destroy(gameObject);
    }
}
