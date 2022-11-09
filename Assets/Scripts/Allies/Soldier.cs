using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    public UnityEvent death = new UnityEvent();

    [Header("Attributes")]
    public int _health;
    public int _damage;
    public float attackRange;
    [SerializeField] private float _detectionRadius;

    [Header("Animation Settings")]
    public float waitTillHit;
    public float waitAfterHit;

    [Header("Setup Fields")]
    [SerializeField] private string _targetTag;
    [SerializeField] private GameObject _target;
    [SerializeField] private Transform[] wanderPoints;
    private NavMeshAgent _agent;
    private Animator animator;
    private bool IsDead = false;
    private bool IsAttacking = false;
    private bool IsWandering = false;

    private void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(_targetTag);
        float shortetsDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < shortetsDistance)
            {
                shortetsDistance = distanceToTarget;
                nearestTarget = target;
            }
            if (nearestTarget != null && shortetsDistance <= _detectionRadius && !IsDead)
            {
                _target = nearestTarget;
                _agent.SetDestination(nearestTarget.transform.position);
            }
            else
            {
                _target = null;
            }
        }
        if (_target == null && !IsDead && !IsWandering)
        {
            StartWandering(wanderPoints);
        }
        else
        {
            StopCoroutine("Wander");
            IsWandering = false;
        }
    }

    public void StartWandering(Transform[] _wanderPoints)
    {
        wanderPoints = _wanderPoints;
        StartCoroutine(Wander(wanderPoints));
    }

    IEnumerator Wander(Transform[] wanderPoints)
    {
        IsWandering = true;
        while (IsWandering)
        {
            for(int i = 0; i < wanderPoints.Length; i++)
            {
                _agent.SetDestination(wanderPoints[i].transform.position);
                yield return new WaitForSeconds(1f);
            }
        }
    }


    public void Update()
    {
        if (_target != null)
        {
            float distanceToCurrentTarget = Vector3.Distance(transform.position, _target.transform.position);
            if (distanceToCurrentTarget <= attackRange)
            {
                animator.SetBool("IsAttacking", true);
                if (IsAttacking == false) StartCoroutine("GiveDamage");
            }
        }
        if (_target == null)
        {
            IsAttacking = false;
            animator.SetBool("IsAttacking", false);
            StopCoroutine("GiveDamage");
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0 && IsDead == false)
        {
            IsDead = true;
            animator.SetTrigger("Death");
            _agent.enabled = false;
            StartCoroutine("DeathAnimation");
        }
    }

    IEnumerator GiveDamage()
    {
        gameObject.transform.LookAt(_target.transform);
        IsAttacking = true;
        while (_target != null)
        {
            yield return new WaitForSeconds(waitTillHit);
            if (_target.GetComponent<Ork>()) _target.GetComponent<Ork>().TakeDamage(_damage);
            yield return new WaitForSeconds(waitAfterHit);
        }
    }

    IEnumerator DeathAnimation()
    {
        death?.Invoke();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _agent.stoppingDistance);
    }
}
