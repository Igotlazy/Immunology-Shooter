using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutrophil : Immune {

    [Header("NEUTROPHIL")]
    [Header("Movement:")]
    public float moveSpeed;
    public float moveInterval;
    public float dashSpeed;

    [Header("Detonation:")]
    public float detonationRadius;
    public LayerMask detonationLayerM;

    protected Vector2 moveVector;
    private bool isDashing;

    public float pullEndDistance; //Cell or Call Cell


    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();

        if (moveInterval <= 0)
        {
            moveInterval = 0.1f;
        }
    }


    protected override void Update()
    {
        base.Update();

        if (!isBeingCalled)
        {
            if (!isDashing)
            {
                NeutrophilMoveRepeat();
            }
        }
        else //go to the player
        {
            float disToPlayer = Vector2.Distance(EventSingleton.ps.transform.position, this.gameObject.transform.position);
            if (disToPlayer > pullEndDistance)
            {
                this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, EventSingleton.ps.transform.position, Time.deltaTime * 5f);
            }
        }
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected void NeutrophilMoveRepeat()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            NeutrophilMove();
            moveTimer = moveInterval;
        }
    }
    float moveTimer;

    protected void NeutrophilMove()
    {
        objectRB2D.velocity = new Vector2(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));
    }

    public void ExplodeReceiver(Collider2D hitEnemy)
    {
        isDashing = true;

        Vector2 enemyDirection = (hitEnemy.gameObject.transform.position.normalized - this.gameObject.transform.position).normalized;
        float enemyDistance = Vector2.Distance(this.gameObject.transform.position, hitEnemy.gameObject.transform.position);

        float timeToBoom = enemyDistance / dashSpeed;
        Vector2 velocity = enemyDirection * dashSpeed;
        objectRB2D.velocity = velocity;

        Invoke("NeutrophilExplode", timeToBoom);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isDashing = true;
            Vector2 enemyDirection = (collision.gameObject.transform.position.normalized - this.gameObject.transform.position).normalized;
            float enemyDistance = Vector2.Distance(this.gameObject.transform.position, collision.gameObject.transform.position);
            float timeToBoom = enemyDistance / dashSpeed;
            objectRB2D.velocity = enemyDirection * dashSpeed;
            Invoke("NeutrophilExplode", timeToBoom);
        }
    }

    protected void NeutrophilExplode()
    {
        Attack explodeAttack = new Attack(10f, this.gameObject);

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(this.transform.position, detonationRadius, detonationLayerM);

        foreach(Collider2D currentCol in enemiesInRange)
        {
            LivingObject livingScript = currentCol.GetComponent<LivingObject>();
            {
                if (livingScript != null)
                {
                    livingScript.ObjectHit(explodeAttack);
                    livingScript = null;
                }
            }
        }
        Debug.Log("Neutrophil Explosion");
        ObjectDeath();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, detonationRadius);
    }
}
