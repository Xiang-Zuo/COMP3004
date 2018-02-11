using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCard : Card {
    public int atk;

    public RankCard(string aName, Kind aKind, int anAtk)
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
