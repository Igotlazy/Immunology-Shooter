using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy {

    [Header("VIRUS")]
    public bool isInfectious;

    [Range(0f, 100f)]
    public float infectiousRate;

    public float pullEndDistance;
    public ParticleSystem infectiousParticles;
    public List<Immune> cellsInRange;
    public Immune currentImmuneScript;

    protected override void Awake()
    {
        float chosenfloat = Random.Range(0f, 100f);
        if (chosenfloat <= infectiousRate)
        {
            isInfectious = true;
            infectiousParticles.Play();
        }

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isInfectious)
        {
            if (cellsInRange.Count <= 0)
            {
                VirusMoveRepeat();
            }
            else
            {
                objectRB2D.velocity = new Vector2(0f, 0f);
                float disToCell = Vector2.Distance(cellsInRange[0].transform.position, this.gameObject.transform.position);

                if (disToCell > pullEndDistance)
                {
                    this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, cellsInRange[0].transform.position, Time.deltaTime * 2f);
                }
                else
                {
                    InfectCell();
                }
            }

            if (cellsInRange.Count > 0)
            {
                if (cellsInRange[0] != null && currentImmuneScript == null)
                {
                    CellPicker();
                }

                if (cellsInRange[0] != null && currentImmuneScript != null)
                {
                    bool targetInfectedStatus = currentImmuneScript.isInfected;

                    if (targetInfectedStatus)
                    {
                        cellsInRange.RemoveAt(0);
                        CellPicker();
                    }
                }
            }
        }
        else
        {
            VirusMoveRepeat();
        }    

        base.Update();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    protected void VirusMoveRepeat()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            VirusMove();
            moveTimer = Random.Range(1.5f, 4f);
        }
    }
    float moveTimer;

    protected void VirusMove()
    {
        objectRB2D.velocity = new Vector2(Random.Range(-6f, 6f), Random.Range(-6f, 3f));
    }

    public void CellPicker()
    {
        Debug.Log("CellPicker called.");
        if (cellsInRange.Count > 0)
        {
            if (cellsInRange[0] != null)
            {
                Debug.Log("Setting current ImmuneScript");
                currentImmuneScript = cellsInRange[0];
            }
            else
            {
                cellsInRange.RemoveAt(0);
                currentImmuneScript = null;
                CellPicker();
            }
        }

    }

    public void InfectCell()
    {
        currentImmuneScript.isInfected = true;
        Destroy(this.gameObject);
    }
}
