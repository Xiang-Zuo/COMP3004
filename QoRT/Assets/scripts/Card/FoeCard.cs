using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FoeCard : Card
{
   // Card aFoe;
    public int atk;
    public int atkSpecial=-1;

    public void print()
    {
        Debug.Log("atk" + atk);
    }

    void Start()
    {
        FoeCard aFoe = new FoeCard();
       
        aFoe.name = "aaa";
        aFoe.atk = 10;
        Debug.Log(aFoe.name);
        aFoe.print();

    }

}