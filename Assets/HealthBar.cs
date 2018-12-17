using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = originalScale * currentHealth / maxHealth;
        gameObject.transform.localScale = scale;
    }
}
