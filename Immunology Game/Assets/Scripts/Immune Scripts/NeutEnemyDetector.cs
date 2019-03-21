using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutEnemyDetector : MonoBehaviour {

    public GameObject parentNeut;
    private Neutrophil neutScript;


	void Start ()
    {
        neutScript = parentNeut.GetComponent<Neutrophil>();	
	}

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            neutScript.ExplodeReceiver(collision);
        }
    }
}
