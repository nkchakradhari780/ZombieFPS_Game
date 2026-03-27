using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float ObjectHealth = 30f;

    public void ObjectHitDamage(float damage)
    {
        ObjectHealth -= damage;
        
        if (ObjectHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
