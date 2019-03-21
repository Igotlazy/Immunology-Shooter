using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusDetector : MonoBehaviour {

    public GameObject parentVirus;
    private Virus virusScript;


	void Start ()
    {
        virusScript = parentVirus.GetComponent<Virus>();
        if (!(virusScript.isInfectious))
        {
            this.gameObject.SetActive(false);
        }
	}
	

    private void OnTriggerEnter2D(Collider2D enteredCollider)
    {
        if (enteredCollider.gameObject.tag == "Ally")
        {
            Immune immuneScript = enteredCollider.GetComponent<Immune>();

            if (immuneScript != null && !immuneScript.isInfected)
            {
                virusScript.cellsInRange.Add(immuneScript);
                immuneScript = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D exitedCollider)
    {
        if (exitedCollider.gameObject.tag == "Ally")
        {
            Immune immuneScript = exitedCollider.GetComponent<Immune>();
            virusScript.cellsInRange.Remove(immuneScript);
        }
    }
}
