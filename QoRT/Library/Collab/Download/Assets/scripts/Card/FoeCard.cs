using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeCard : Card
{
    private int initialATK;
    private int atk;
    private int atkSpecial;
    //private Ability ability;

    public FoeCard(string aName, Kind aKind, int anAtk, int anAtkSpecial)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.atk = anAtk;
        initialATK = atk;
        this.atkSpecial = anAtkSpecial;
        //this.ability = anAbility;
    }
    public int getAtk()
    {
        return this.atk;
    }

    public int getAtkSpecial()
    {
        return this.atkSpecial;
    }
    
    public void setAtk(int newAtk)
    {
        atk = newAtk;
    }

    public void initialTheATK()
    {
        atk = initialATK;
    }

}