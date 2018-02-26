using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankCard : Card {
    private Rank rank;

    public RankCard(string aName, Kind aKind, Rank aRank)
    {
        this.setName(aName);
        this.setKind(aKind);
        rank = aRank;

    }
   
    Rank getRank()
    {
        return rank;
    }
}
