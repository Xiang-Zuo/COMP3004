using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FoeCard : Card
{
   // Card aFoe;
    public int atk { get; set; }
    public int atkSpecial { get; set; }

    public void printOn()
    {
        Debug.Log("atk" + atk);
    }

}