using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {
    
    private int teamNumber;
    private Color teamColor;

    public Renderer body;
    public float maxHealth;
    public float respawnTime;
    public float currentHealth { get; private set; }

    private void Start()
    {
        //set the team randomly
        teamNumber = Random.Range(1, 3);

        //set the team color based on the team the player is in
        if(teamNumber == 1)
        {
            teamColor = Color.blue;
        }else if(teamNumber == 2)
        {
            teamColor = Color.red;
        }

        //apply the color
        body.material.color = teamColor;
        SetDefaults();
    }

    public void TakeDamage(float amount, Target source)
    {
        if (source.teamNumber == teamNumber)
            return;

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
