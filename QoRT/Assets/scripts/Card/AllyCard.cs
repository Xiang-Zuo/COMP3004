using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCard : Card {

    private int atk;
    private int bidNum;
   // private Ability ability;

    public AllyCard(string aName, Kind aKind, int anAtk, int aBidNum)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.atk = anAtk;
        this.bidNum = aBidNum;
        //this.ability = anAbility;
    }
    public int getAtk()
    {
        return this.atk;
    }

    public int getBidNum()
    {
        return this.bidNum;
    }
   /* public Ability getAbility()
    {
        return this.ability;
    }*/

}
