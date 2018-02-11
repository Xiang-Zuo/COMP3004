using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmourCard : Card
{
    private int atk;

    public AmourCard(string aName, Kind aKind, int anAtk)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.atk = anAtk;
    }
    public int getAtk()
    {
        return this.atk;
    }

}
