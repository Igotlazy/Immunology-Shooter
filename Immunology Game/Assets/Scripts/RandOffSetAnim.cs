using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVariation : MonoBehaviour {

    [Header("OFFSET:")]
    public bool randomOffset;
    public float chosenOffset;
    public float minOffset = 0f;
    public float maxOffset = 1f;

    [Header("LOOP SPEED:")]
    public bool randomLoopSpeed;
    public float minLoopSpeedMultiplier = 1f;
    public float maxLoopSpeedMultiplier = 0.6f;
    public float chosenLoopSpeedMultiplier;


    private void Awake()
    {
        if (randomOffset)
        {
            chosenOffset = Random.Range(minOffset, maxOffset);
            GetComponent<Animator>().SetFloat("offSet", chosenOffset);
        }

        if (randomLoopSpeed)
        {
            chosenLoopSpeedMultiplier = Random.Range(minLoopSpeedMultiplier, maxLoopSpeedMultiplier);

            //Just to make sure there isn't a speed multiplier of 0 for an animation. 
            chosenLoopSpeedMultiplier = Mathf.Clamp(chosenLoopSpeedMultiplier, 0.01f, 100f);
            GetComponent<Animator>().SetFloat("loopSpeed", chosenLoopSpeedMultiplier);
        }


    }
}
