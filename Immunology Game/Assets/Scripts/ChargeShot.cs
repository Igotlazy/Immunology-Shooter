using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot : MonoBehaviour {

    public float fireRate;
    public float chargeRate;
    public GameObject laser;
    public Transform weaponSpawner;

    private float nextFire = 0.0f;
    private float charge = 0;
    private float chargeMax = 100;
    private bool chargeStarted = false;

    GameObject projectile;
	
	void Update () {

		if (Input.GetKeyDown("space")) //charge
        {
            projectile = Instantiate(laser, weaponSpawner.position, weaponSpawner.rotation) as GameObject;
            projectile.transform.parent = weaponSpawner.transform;
            chargeStarted = true;
 
            while (charge < chargeMax)
            {

            }
        }
        else if (Input.GetKeyUp("space")) //release shot
        {
            //StandardFire();
            charge = 0;
            Destroy(projectile);
            chargeStarted = false;
        }
        if (chargeStarted == true)
        {
            if (charge < chargeMax)
            {
                charge += chargeRate * Time.deltaTime;
                //visual charge
                projectile.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            }

        }
	}

    private void StandardFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject projectile = Instantiate(laser, weaponSpawner.position, weaponSpawner.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 3000);
            //projectile.GetComponent<Projectile>().attackObject = new Attack(laserDamage, this.gameObject);
        }
    }
}
