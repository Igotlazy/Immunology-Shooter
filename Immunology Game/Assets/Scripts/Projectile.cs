using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileLifeTime;
    public bool destroyOnImpact;
    public Attack attackObject;

    void Start()
    {
        Invoke("DestroySelf", projectileLifeTime);
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitCollider)
    {
        if (hitCollider.tag == "Enemy" && hitCollider.gameObject.activeInHierarchy)
        {
            GameObject hitObject = hitCollider.gameObject;

            LivingObject enemyScript = hitObject.GetComponent<LivingObject>();

            if (enemyScript != null)
            {
                enemyScript.ObjectHit(attackObject);
            }

            if (destroyOnImpact)
            {
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
