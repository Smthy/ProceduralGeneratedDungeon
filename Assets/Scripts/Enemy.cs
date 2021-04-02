using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject death;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //GameObject deathGO = Instantiate(death, transform.position, Quaternion.identity);
        //Destroy(deathGO, 0.5f);
        Destroy(gameObject);
    }

}
