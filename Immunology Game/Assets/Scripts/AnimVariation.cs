using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandOffSetAnim : MonoBehaviour {

    public float chosenOffSet;

    private void Awake()
    {
        chosenOffSet = Random.Range(0.0f, 1.0f);
        GetComponent<Animator>().SetFloat("offSet", chosenOffSet);
    }
}
