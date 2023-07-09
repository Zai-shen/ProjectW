using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Health))]
public class CinnamonRoll : Enemy
{
    public ParticleSystem[] OnDeathEffects;

    public float ExplosionRadius = 5f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = MoveSpeed;
        _navMeshPath = new NavMeshPath();
        Health = GetComponent<Health>();
        Health.OnDeath += Die;
    }
    
    private void Start()
    {
        _target = PlayerManager.Instance.ABaker.transform;
    }

    private void Update()
    {
        if (dead)
            return;

        _searchCooldown += Time.deltaTime;
        if (_searchCooldown >= SearchCooldown)
        {
            if (!_attackOnCooldown) 
                ChasePlayer();
            _searchCooldown -= SearchCooldown;
        }

        PlayerInAttackRange = CheckTargetInAttackRange();
        if (PlayerInAttackRange && !_attackOnCooldown) Attack();
    }

    private void Attack()
    {
        Die();
    }

    private void ChasePlayer()
    {
        if (_agent.CalculatePath(_target.position, _navMeshPath))
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    }
    
    [ContextMenu("Death")]
    void Die()
    {
        dead = true;
        _agent.isStopped = true;
        foreach (ParticleSystem ps in OnDeathEffects)
        {
            ps.Play();
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Health>(out Health hp))
            {
                hp.TryTakeDamage(AttackDamage);
            }
        }

        StartCoroutine(KillAfterSeconds(1));
    }

    private IEnumerator KillAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (UseAggroRange)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, AggroRange);
        }

        if (UsePatroling)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, WalkPoint);
        }
        
        if (PlayerInSight)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _target ? _target.position : Vector3.zero );
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
