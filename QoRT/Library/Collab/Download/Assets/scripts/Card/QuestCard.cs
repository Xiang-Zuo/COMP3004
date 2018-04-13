using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCard : Card {
    private int stageNum;
    //private Ability ability;

    public QuestCard(string aName, Kind aKind, int aStageNum)
    {
        this.setName(aName);
        this.setKind(aKind);
        this.stageNum = aStageNum;
        //this.ability = anAbility;
    }
    public int getStageNum()
    {
        return this.stageNum;
    }
		

    /*public Ability getAbility()
    {
        return this.ability;
    }*/

}
