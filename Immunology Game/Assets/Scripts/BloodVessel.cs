using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVessel : MonoBehaviour {

    public float startingHealth;
    public float currentHealth;
    public float damageInterval;
    public List<GameObject> enemyArray;

    private float nextTakeDamage;


	// Use this for initialization
	void Start ()
    {
        currentHealth = startingHealth;		
	}
	
	// Update is called once per frame
	void Update ()
    {
        TakeDamage();
	}

    private void TakeDamage()
    {
        if (Time.time > nextTakeDamage)
        {
            nextTakeDamage = Time.time + damageInterval;
            float amountOfEnemies = enemyArray.Count;
            currentHealth -= amountOfEnemies * 0.5f;
            currentHealth = Mathf.Clamp(currentHealth, 0f, startingHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderEnter)
    {
        if (colliderEnter.gameObject.tag == "Enemy")
        {
            enemyArray.Add(colliderEnter.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D colliderExit)
    {
        if (colliderExit.gameObject.tag == "Enemy")
        {
            enemyArray.Remove(colliderExit.gameObject);
        }
    }
}
