using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy {

    [Header("BACTERIA")]
    [Header("Divide Properties:")]
    public float divideTimeMax = 3;
    public float divideTimeMin = 2;
    private float divideTimeCounter;
    public float divideSpeed;
    public float divideDuration;
    private float divideDurationCounter;

    [Header("State Bools:")]
    public bool isDividing;
    public bool isMoving;

    [Header("GO References:")]
    public GameObject daughterBacteria;
    public GameObject dividePointer;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        //objectCol2D.enabled = true;
        divideTimeCounter = Random.Range(divideTimeMin, divideTimeMax);
        divideDurationCounter = divideDuration;

        base.Start();
    }
	
	
	protected override void Update()
    {
        base.Update();

        if (isMoving)
        {
            BacteriaMoveRepeat();
        }

        BacteriaDivide();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }



    //-----------[METHODS]------------------------------------------------------------------------------------------------//

    protected void BacteriaMoveRepeat()
    {
        moveTimer -= Time.deltaTime; 
        if (moveTimer <= 0)
        {
            BacteriaMove();
            moveTimer = 3f;
        }
    }
    float moveTimer;

    protected void BacteriaMove()
    {
        objectRB2D.velocity = new Vector2(Random.Range(-12f, 12f), Random.Range(-12f, 6f));
    }

    protected void BacteriaDivide()
    {
        if (divideTimeCounter >= 0)
        {
            divideTimeCounter -= Time.deltaTime;
        }
        else
        {
            this.isMoving = false;
            this.isDividing = true;

            if (this.hasDivided == false)
            {
                objectRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                objectRB2D.mass = 99f;

                spawnedDaughter = Instantiate(daughterBacteria, this.gameObject.transform.position, this.gameObject.transform.rotation);
                daughterRB2D = spawnedDaughter.GetComponent<Rigidbody2D>();
                daughterScript = spawnedDaughter.GetComponent<Bacteria>();

                daughterRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                daughterRB2D.mass = 99f;
                spawnedDaughter.layer = 10;

                Vector3 divideVelocity = (dividePointer.transform.position - this.gameObject.transform.position).normalized * divideSpeed;

                objectRB2D.velocity = divideVelocity;
                daughterRB2D.velocity = -divideVelocity;

                this.hasDivided = true;
            }

            divideDurationCounter -= Time.deltaTime;
            if (divideDurationCounter <= 0 && this.hasDivided == true)
            {
                //Cleanup

                objectRB2D.constraints = RigidbodyConstraints2D.None;
                objectRB2D.mass = 1f;

                if (spawnedDaughter != null)
                {
                    daughterRB2D.constraints = RigidbodyConstraints2D.None;
                    spawnedDaughter.layer = 9;
                    daughterRB2D.mass = 1f;
                    daughterScript.isMoving = true;
                }
     

                this.isDividing = false;
                this.hasDivided = false;
                this.isMoving = true;
                divideTimeCounter = Random.Range(divideTimeMin, divideTimeMax);
                divideDurationCounter = divideDuration;

                moveTimer = 0f;
            }
        }
    }

    GameObject spawnedDaughter;
    Rigidbody2D daughterRB2D;
    Collider2D daughterCol2D;
    Bacteria daughterScript;
    [SerializeField]
    bool hasDivided;
}
