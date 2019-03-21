using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public float pickupSpeed;
    public float shrinkRate;
    private bool isPickedUp;
    private Transform xform;

    private void Start()
    {
        isPickedUp = false;
        xform = GetComponent<Transform>();
    }

    public void GetPickedUp()
    {
        isPickedUp = true;
    }

    void Update()
    {

        //When player picks up
        if (isPickedUp)
        {
            //Get drawn to player ship
            xform.position = Vector2.MoveTowards(xform.position, EventSingleton.ps.transform.position, pickupSpeed * Time.deltaTime);
            //Shrink pickup item
            xform.localScale -= new Vector3(shrinkRate, shrinkRate, 0);
            //Add sound effect

            //Then get rid of pickup gameobject
            Destroy(this.gameObject, 0.25f);

        }
    }
}
