using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class DoISponsorAQuestB : AbstractAI
{
    private QuestGame.Logger strategyBLog = new QuestGame.Logger(true);

    public override List<Card>[] DoISponsorAQuest(QuestCard aQuest, Player[] players, Player theSponsor)
    {
        List<Card>[] sponsorStages = new List<Card>[aQuest.getStageNum()];
        //Debug.Log(aQuest.getStageNum());
        strategyBLog.info("Now is in AIStrategyB, " + theSponsor.getName() + " is the Sponsor");
        bool evolve = false;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].getName() == theSponsor.getName())
            {
            }
            else
            {
                if (players[i].getRank() == Rank.SQUIRE && (players[i].getShield().Count + aQuest.getStageNum()) >= 50)
                {
                    evolve = true;
                    strategyBLog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
                }
                else if (players[i].getRank() == Rank.KNIGHT && (players[i].getShield().Count + aQuest.getStageNum()) >= 70)
                {
                    evolve = true;
                    strategyBLog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
                }
                else if (players[i].getRank() == Rank.CHAMPIONKNIGHT && (players[i].getShield().Count + aQuest.getStageNum()) >= 100)
                {
                    evolve = true;
                    strategyBLog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
                }
                else evolve = false;
            }
            if (evolve == true)
            {
                strategyBLog.info("AIStrategyB ends here\n");
                return null;
            }
        }
        //strategyBLog.info("start");
        if (validFoe(aQuest, theSponsor.getHand()) == false)
        {
            strategyBLog.info("no enough foes");
            return null;
        }else
        {
            strategyBLog.info("has enough foes");
        }



        return sponsorStages;
    }

    private bool validFoe(QuestCard aQuestCard, List<Card> sponsorHand)
    {
        if (aQuestCard.getName().Contains("SFTHG") == true) Debug.Log("ffffffff");
        Debug.Log(aQuestCard.getName());
        int[] miniumATK = { 5, 15, 30, 50 };
        List<Card>[] sponsorStage = new List<Card>[aQuestCard.getStageNum()];
        List<FoeCard> theFoeList = new List<FoeCard>();
        List<FoeCard> sortedFoeList;
        bool hasTestCard = false;
        int totalATK;                 //add all cards' atk without duplicate value
        int diffCardCount=0;            //calculate the card's number (not conatin two card has same atk value)
        for (int i = 0; i < sponsorHand.Count; i++)
        {
            if (sponsorHand[i].getKind() == Kind.FOE)
            {
                diffCardCount = 1;
                FoeCard theFoe = (FoeCard)sponsorHand[i];
                theFoe.initialTheATK();
                if (aQuestCard.getName().Contains("JTTEF"))
                    if (theFoe.getName().Contains("Evil"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTSR"))
                    if (theFoe.getName().Contains("Saxons"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("BH"))
                    if (theFoe.getName().Contains("Boar"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("DTQH"))
                    theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("STD"))
                    if (theFoe.getName().Contains("Dragon"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("RTFM"))
                    if (theFoe.getName().Contains("Black"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                if (aQuestCard.getName().Contains("SFTHG"))
                        theFoe.setAtk(theFoe.getAtkSpecial()); 
                if (aQuestCard.getName().Contains("TOTGK"))
                    if (theFoe.getName().Contains("Green"))
                        theFoe.setAtk(theFoe.getAtkSpecial());
                else { } 
                theFoeList.Add(theFoe);
            }
            if (sponsorHand[i].getKind() == Kind.TEST)
                hasTestCard = true;
        }
        sortedFoeList = theFoeList.OrderBy(x => x.getAtk()).ToList();
        totalATK = sortedFoeList[0].getAtk();
        for (int i=1; i < sortedFoeList.Count; i++)
        {
            Debug.Log(sortedFoeList[i].getName() + " " + sortedFoeList[i].getAtk());
            if (sortedFoeList[i].getAtk()!= sortedFoeList[i - 1].getAtk())
            {
                totalATK += sortedFoeList[i].getAtk();
                diffCardCount += 1;
            }
        }
        if (hasTestCard == true)
        {
            if (aQuestCard.getStageNum() == 2)
            {
                if (totalATK < 40 || diffCardCount < (aQuestCard.getStageNum() - 1))
                    return false;
                else return true;
            }
            else
            {
                if (totalATK < (40 + miniumATK[aQuestCard.getStageNum() - 3]) || diffCardCount < (aQuestCard.getStageNum() - 1))
                    return false;
                else return true;
            }
        }
        else
        {
            if (totalATK < (40 + miniumATK[aQuestCard.getStageNum() - 2]) || diffCardCount < aQuestCard.getStageNum())
                return false;
            else return true;
        }     
    }

}


