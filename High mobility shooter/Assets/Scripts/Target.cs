using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

    public float maxHealth;
    public float respawnTime;
    public float currentHealth { get; private set; }

    private void Start()
    {
        SetDefaults();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    private void Die()
    {
        Renderer render = GetComponent<Renderer>();
        if(render != null)
        {
            render.enabled = false;
        }

        Collider col = GetComponent<Collider>();
        if(col != null)
        {
            col.enabled = false;
        }

        StartCoroutine(respawn());
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        SetDefaults();

        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.enabled = true;
        }
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }
}
