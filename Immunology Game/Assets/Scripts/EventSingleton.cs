using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventSingleton
{

    //Refer to the playerscript from anywhere through EventSingleton
    public static PlayerScript ps;

    //Player inventory
    public static int antigens = 0;

    public static void AddAntigen()
    {
        Debug.Log("Collected an Antigen.");
        antigens++;
    }

    public static void SubtractAntigens(int takenAntigens)
    {
        antigens -= takenAntigens;
        if (antigens < 0) //don't go below zero Ags
            antigens = 0;
    }

}
