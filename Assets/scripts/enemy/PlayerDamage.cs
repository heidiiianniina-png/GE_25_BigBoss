using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damageAmount = 25;
    public float attackRange = 3f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, attackRange))
            {
                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }
}
