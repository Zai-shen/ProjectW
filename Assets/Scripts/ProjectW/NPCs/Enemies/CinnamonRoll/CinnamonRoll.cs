using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Health))]
public class CinnamonRoll : MonoBehaviour
{
    public ParticleSystem[] OnDeathEffects;

    #region Patroling

    public bool UsePatroling = false;
    public bool UseAggroRange = false;
    public float AggroRange = 10f;
    public Vector3 WalkPoint;
    private bool _walkPointSet;
    public float WalkPointRange;
    
    #endregion

    #region Health

    public Health Health;
    private bool dead;

    #endregion
    
    #region States

    public bool PlayerInSight, PlayerInAttackRange;

    #endregion

    #region Navigation

    public float SearchCooldown = 0.1f;
    private float _searchCooldown;
    public float MoveSpeed = 2f;
    private Transform _target;
    private NavMeshAgent _agent;
    private NavMeshPath _navMeshPath;
    
    #endregion

    public int AttackDamage = 20;
    public float AttackRange = 1f;
    public float ExplosionRadius = 5f;
    private bool _attackOnCooldown;

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

    private void OnEnable()
    {
        Globals.Enemies.Add(this.gameObject);
    }

    private void OnDisable()
    {
        Globals.Enemies.Remove(this.gameObject);
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

    public float DistanceToTarget()
    {
        return Vector3.Distance(_target.position, transform.position);
    }

    public bool CheckTargetInAttackRange()
    {
        return DistanceToTarget() <= AttackRange;
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

    private void Patrole()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet) _agent.SetDestination(WalkPoint);

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float _randomX = Random.Range(-WalkPointRange, WalkPointRange);
        float _randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        const float _height = 2f;
        Vector3 _difference = new(_randomX, _height, _randomZ);

        WalkPoint = transform.position + _difference;
        
        if (_agent.CalculatePath(WalkPoint, _navMeshPath))
        {
            _walkPointSet = true;
        }else if (PlayerInAttackRange)
        {
            _walkPointSet = true;
        }
    }

    private void FaceTarget()
    {
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
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
