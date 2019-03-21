using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour
{
    [Header("LIVING OBJECT")]
    [Header("Properties:")]
    public float startingHealth;
    public float currentHealth;    
    public float invincibilityDuration;
    private float invincibilityCounter;

    [Header("State Bools:")]
    public bool isInvincible;

    [Header("MISC:")]
    public GameObject deathParticles;
    protected Rigidbody2D objectRB2D;
    protected Collider2D objectCol2D;



    protected virtual void Awake()
    {
        this.objectRB2D = GetComponent<Rigidbody2D>();
        this.objectCol2D = GetComponent<Collider2D>();
    }


    protected virtual void Start()
    {
        this.currentHealth = this.startingHealth;
    }


    protected virtual void Update()
    {

    }


    protected virtual void FixedUpdate()
    {

    }

    //-----------[METHODS]------------------------------------------------------------------------------------------------//

    public void ObjectHit(Attack receivedAttack)
    {
        if (!isInvincible)
        {
            this.currentHealth -= receivedAttack.damageValue;

            this.currentHealth = Mathf.Clamp(this.currentHealth, 0.0f, this.startingHealth);

            if (this.currentHealth <= 0)
            {
                ObjectDeath();
            }

            else
            {
                invincibilityCounter = invincibilityDuration;
            }
        }
    }


    public void ObjectDeath()
    {
        if (this.gameObject.tag == "Player")
        {
            //Not decided yet.
            Debug.Log("Player - Death");
        }

        else
        {
            Debug.Log(this.gameObject.name + " just died.");
            Instantiate(deathParticles, this.transform.position, deathParticles.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    protected void ObjectTimekeeping()
    {
        //Invincibility
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
    }
}
