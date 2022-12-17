using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 50f;
    [SerializeField] float turnSpeed = 5f;
    
    bool isProvoked = false;
    float distanceToTarget = Mathf.Infinity;

    NavMeshAgent navMeshAgent;
    EnemyHealth enemyHealth;



    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (enemyHealth.IsDead())
        {
            enabled = false; //desabilita la IA si esta muerto
            navMeshAgent.enabled = false; //desabilita la navegacion, para que no se deslice por el suelo.
            GetComponent<CapsuleCollider>().enabled = false;
            return;

        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTraget();
        }
        else if (distanceToTarget <= chaseRange) //si la distancia hacia el blanco player es menor o igual al rango de persecucion, el enemigo persigue
        {
            isProvoked = true;
            //navMeshAgent.SetDestination(target.position);
        }

    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }
    private void EngageTraget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget<= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true); 
        Debug.Log(name + " is seeking and destroying" + target.name);
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack",false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime* turnSpeed); 
    }

    void OnDrawGizmosSelected()
    {
        // Muestra el radio de persecucion
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
