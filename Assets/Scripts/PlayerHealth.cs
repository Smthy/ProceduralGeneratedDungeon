using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int projectileDamage = 10;

    public HealthBar healthBar;

    public AudioSource playerDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {          
            StartCoroutine("Switch");                        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Arrow"))
        {
            currentHealth -= projectileDamage;
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void IncreaseHealth(int heal)
    {
        currentHealth += heal;

        healthBar.SetHealth(currentHealth);
    }

    IEnumerator Switch()
    {
        playerDeath.Play();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("End Screen");
    }
}
