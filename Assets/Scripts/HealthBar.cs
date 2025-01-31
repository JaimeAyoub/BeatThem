using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet = new Vector3 (0, 2,0);
    public Slider healthSlider;
    void Start()
    {
        transform.position = target.position + offSet;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
