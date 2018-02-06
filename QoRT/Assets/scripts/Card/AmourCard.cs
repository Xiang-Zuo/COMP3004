using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmourCard : Card
{
    private int atk;
    private int bidNum;

    public AmourCard(string aName, Kind aKind, int anAtk, int aBidNum)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.atk = anAtk;
        this.bidNum = aBidNum;
    }
    public int getAtk()
    {
        return this.atk;
    }

    public int getBidNum()
    {
        return this.bidNum;
    }

}
