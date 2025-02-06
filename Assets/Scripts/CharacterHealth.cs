using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
  
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject healthBarPrefab; // Prefab de la barra de vida

    private HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        // Instanciar la barra de vida
        if (healthBarPrefab != null)
        {
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar = healthBarInstance.GetComponent<HealthBar>();
            healthBar.target = transform; // Asignar este personaje como objetivo
        }
        else
        {
            Debug.LogError("Health Bar Prefab no está asignado.");
        }
    }

    void Update()
    {
        // Ejemplo: Reducir la vida con la tecla "H" (para pruebas)
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de vida
        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth, maxHealth);
        }

        // Si la vida llega a 0, el personaje muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Personaje muerto");
        // Aquí puedes agregar lógica de muerte (animación, sonido, etc.)
        Destroy(gameObject);
    }
}

