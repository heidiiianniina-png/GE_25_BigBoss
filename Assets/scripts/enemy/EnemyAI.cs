using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player; // auto-found if not assigned
    private NavMeshAgent agent;

    [Header("Stats")]
    public float detectionRadius = 15f;
    public float loseSightRadius = 20f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.2f;
    public float stoppingDistance = 1.5f;

    [Header("Line of Sight")]
    public LayerMask obstacleMask;
    public float eyeHeight = 1.2f;

    private bool isChasing = false;
    private bool canAttack = true;
    private Transform myTransform;
    private float sqrDetectionRadius, sqrLoseSightRadius, sqrAttackRange;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;

        // Auto-find player by tag if not set
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        // Cache squared distances
        sqrDetectionRadius = detectionRadius * detectionRadius;
        sqrLoseSightRadius = loseSightRadius * loseSightRadius;
        sqrAttackRange = attackRange * attackRange;

        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - myTransform.position;
        float sqrDist = new Vector3(toPlayer.x, 0, toPlayer.z).sqrMagnitude;

        // Check detection / lose sight
        if (!isChasing)
        {
            if (sqrDist <= sqrDetectionRadius && HasLineOfSight())
                isChasing = true;
        }
        else
        {
            if (sqrDist > sqrLoseSightRadius || !HasLineOfSight())
            {
                isChasing = false;
                agent.ResetPath();
            }
        }

        // Behavior
        if (isChasing)
        {
            agent.SetDestination(player.position);

            if (sqrDist <= sqrAttackRange)
            {
                agent.isStopped = true;
                FaceTarget();

                if (canAttack)
                    StartCoroutine(DoAttack());
            }
            else
            {
                agent.isStopped = false;
            }
        }
    }

    bool HasLineOfSight()
    {
        if (player == null) return false;

        Vector3 from = myTransform.position + Vector3.up * eyeHeight;
        Vector3 to = player.position + Vector3.up * eyeHeight;
        Vector3 dir = to - from;
        float dist = dir.magnitude;
        dir.Normalize();

        if (Physics.Raycast(from, dir, dist, obstacleMask))
            return false;

        return true;
    }

    void FaceTarget()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude > 0.001f)
        {
            Quaternion look = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, look, 10f * Time.deltaTime);
        }
    }

    IEnumerator DoAttack()
    {
        canAttack = false;

        // Debug check to make sure it’s actually running
        Debug.Log($"{name} attacks player for {attackDamage} damage!");

        // Give a short delay before dealing damage if you want an attack wind-up
        yield return new WaitForSeconds(0.2f);

        // DAMAGE FIX — now properly searches player hierarchy for PlayerHealth
        PlayerHealth ph = player.GetComponentInChildren<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(attackDamage);
        }
        else
        {
            Debug.LogWarning($"{name} could not find PlayerHealth on {player.name}");
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, loseSightRadius);
    }
}
