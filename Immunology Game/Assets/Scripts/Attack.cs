using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public float damageValue;
    public GameObject damageSource;


    public Attack(float damageValue, GameObject damageSource)
    {
        this.damageValue = damageValue;
        this.damageSource = damageSource;
    }
}
