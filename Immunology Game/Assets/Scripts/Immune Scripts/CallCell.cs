using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCell : MonoBehaviour {

    private Immune immuneScript;
    public List<GameObject> cellsInRange;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D enteredCol)
    {
        if (enteredCol.gameObject.tag == "Ally")
        {
            immuneScript = enteredCol.gameObject.GetComponent<Immune>();
            if (immuneScript != null)
            {
                cellsInRange.Add(enteredCol.gameObject);
                immuneScript.isBeingCalled = true;
                immuneScript = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D exitedCol)
    {
        if (exitedCol.gameObject.tag == "Ally")
        {
            immuneScript = exitedCol.gameObject.GetComponent<Immune>();
            if (immuneScript != null)
            {
                cellsInRange.Remove(exitedCol.gameObject);
                immuneScript.isBeingCalled = false;
                immuneScript = null;
            }
        }
    }

    public void DeCallCells()
    {
        foreach(GameObject currentObj in cellsInRange)
        {
            immuneScript = currentObj.gameObject.GetComponent<Immune>();
            if (immuneScript != null)
            {
                immuneScript.isBeingCalled = false;
                immuneScript = null;
            }
        }

        cellsInRange.Clear();
    }
}
