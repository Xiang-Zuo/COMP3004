using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentCard : Card {
    private int bounsNum;

    public TournamentCard(string aName, Kind aKind, int aBounsNum)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.bounsNum = aBounsNum;
    }
    public int getBounsNum()
    {
        return this.bounsNum;
    }
}
