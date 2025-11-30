using UnityEngine;

public class PlayerDamageDebug : MonoBehaviour
{
    public int damageAmount = 25;
    public float attackRange = 3f;
    public Transform attackOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("ATTACK: Left click detected.");

            if (attackOrigin == null)
            {
                Debug.LogError("ATTACK ERROR: No attackOrigin assigned!");
                return;
            }

            Debug.DrawRay(attackOrigin.position, attackOrigin.forward * attackRange, Color.red, 1f);

            RaycastHit hit;
            bool didHit = Physics.Raycast(
                attackOrigin.position,
                attackOrigin.forward,
                out hit,
                attackRange
            );

            if (!didHit)
            {
                Debug.Log("ATTACK: Raycast hit NOTHING.");
                return;
            }

            Debug.Log("ATTACK: Raycast hit: " + hit.collider.name);

            EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

            if (enemy == null)
            {
                Debug.Log("ATTACK: Hit object does NOT have EnemyHealth.");
                return;
            }

            Debug.Log("ATTACK: Enemy found! Dealing " + damageAmount + " damage.");
            enemy.TakeDamage(damageAmount);
        }
    }
}
