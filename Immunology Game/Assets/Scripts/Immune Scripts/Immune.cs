using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immune : LivingObject {

    [Header("CELL")]
    [SerializeField]
    public bool isBeingCalled;
    private bool isRespondingCall;
    public bool isInfected;
    public GameObject infectedCell;
    public GameObject callParticlesObj;
    public ParticleSystem infectedParticles;
    private ParticleSystem.EmissionModule infectedEmission;
    private ParticleSystem callParticles;

    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();

        callParticles = callParticlesObj.GetComponent<ParticleSystem>();
        infectedEmission = infectedParticles.emission;
    }


    protected override void Update()
    {
        base.Update();

        CallParticleControl();

        if (isInfected)
        {
            BecomeInfected();
        }
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    //-----------[METHODS]------------------------------------------------------------------------------------------------//

    public void CallParticleControl()
    {
        if (isBeingCalled)
        {
            if (!isRespondingCall)
            {
                callParticles.Play();
                isRespondingCall = true;
            }

        }
        else
        {
            callParticles.Stop();
            isRespondingCall = false;
        }
    }

    public void BecomeInfected()
    {
        infectedParticles.Play();
        timeSinceInfection += Time.deltaTime;
        infectedEmission.rateOverTime = (timeSinceInfection / 5f);

        if (timeSinceInfection >= 20f)
        {
            Instantiate(infectedCell, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    float timeSinceInfection;


}
