
using UnityEngine;

public class target : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float damageamt ) {
        health -= damageamt;
        if (health<=0){
            Die();
        }
    }

    void Die () 
    {
        Destroy(gameObject);
    }
}
