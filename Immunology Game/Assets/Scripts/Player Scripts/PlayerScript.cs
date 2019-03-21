using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : LivingObject {

    [Header("PLAYER SCRIPT")]

    [Header("Movement:")]
    public float acceleration;
    public float maxSpeed;

    private float curSpeed = 0.0f;
    private float smooth = 5f;
    private float turnAngle = 15f;
    private float turnAxis;

    [Header("Attack:")]
    public GameObject beam;
    public GameObject laser;
    public float laserDamage = 1f;
    public Transform laserSpawner;
    public float fireRate;
    public Transform ship;

    private float nextFire = 0.0f;
    private List<string> weapons;
    private int weaponsIndex;
    private int equippedWeaponNumber; //Converts Weapon Name to integer

    [Header("Cell Call:")]
    [Tooltip("Delay between allowed button press")]
    public float callDelayInterval;
    private float nextCallTime;
    public GameObject callRadiusObj;
    public GameObject callParticlesObj;

    private ParticleSystem callParticles;
    private bool particlesPlaying;
    

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        EventSingleton.ps = this;

        //Weapon Selection
        weapons = new List<string> { "Standard", "Alternate" };
        weaponsIndex = 0;
        equippedWeaponNumber = 0;
        Debug.Log("Equipped: " + weapons[weaponsIndex]);

        //Particle
        callParticles = callParticlesObj.GetComponent<ParticleSystem>();
	}

    protected override void Update()
    {
        //WEAPON CHANGE

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponsIndex++;
            if (weaponsIndex >= weapons.Count)
                weaponsIndex = 0;
            EquippedWeapon();
            Debug.Log("Equipped: " + weapons[weaponsIndex]);
        }

        //SHOOTING
        
        if (Input.GetKey("space"))
        {
            if (equippedWeaponNumber == 0)
                StandardFire();
            else if (equippedWeaponNumber == 1)
                AlternateFire();
        }
        
        //Calling Cells
        CallCells();
        CallCellsCancel();

        base.Update();
    }

    protected override void FixedUpdate()
    {        
        //MOVEMENT

        curSpeed += acceleration;
        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
        else if (curSpeed < 0)
            curSpeed = 0;
        
        if (Input.GetKey("up")) //move (thrusters on)
        {
            objectRB2D.AddRelativeForce(Vector2.up * curSpeed);
        }
        else if (Input.GetKey("down")) //moving backwards is slower
        {
            objectRB2D.AddRelativeForce(Vector2.down * curSpeed * 0.6f);
        }

        turnAxis = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * turnAxis * 3f); //left/right turn
        float angle = Mathf.Lerp(0, -turnAxis * turnAngle, smooth);
        ship.transform.localEulerAngles = new Vector3(0, angle, ship.rotation.z); //slight 3d rotation

        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //on collision with antigen
        if (collision.gameObject.tag == "Antigen")
        {
            collision.gameObject.GetComponent<ItemPickup>().GetPickedUp();
            EventSingleton.AddAntigen();
        }
    }

    //WEAPON TYPES // TYPES OF FIRE >>>>

    private void StandardFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject projectile = Instantiate(laser, laserSpawner.position, laserSpawner.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 3000);
            projectile.GetComponent<Projectile>().attackObject = new Attack(laserDamage, this.gameObject);
        }
    }

    private void AlternateFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject projectile = Instantiate(beam, laserSpawner.position, laserSpawner.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 3000);
        }
    }

    //WEAPON TYPES // TYPES OF FIRE <<<<


    public void AddWeapon(string newWeapon) //add weapon to list for use by player
    {
        weapons.Add(newWeapon);
    }

    private void EquippedWeapon() //Sets what spacebar does in Update
    {
        if (weapons[weaponsIndex].Equals("Standard"))
            equippedWeaponNumber = 0;
        else if (weapons[weaponsIndex].Equals("Alternate"))
            equippedWeaponNumber = 1;
        else if (weapons[weaponsIndex].Equals("Lightning"))
            equippedWeaponNumber = 2;
    }

    private void CallCells()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Time.time > nextCallTime))
        {
            nextCallTime = Time.time + callDelayInterval;
            callRadiusObj.SetActive(true);
            if (!particlesPlaying)
            {
                callParticles.Play();
                particlesPlaying = true;
            }
        }
    }

    private void CallCellsCancel()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            callParticles.Stop();
            particlesPlaying = false;

            CallCell callScript = callRadiusObj.GetComponent<CallCell>();
            callScript.DeCallCells();
            callRadiusObj.SetActive(false);
        }
    }


}
