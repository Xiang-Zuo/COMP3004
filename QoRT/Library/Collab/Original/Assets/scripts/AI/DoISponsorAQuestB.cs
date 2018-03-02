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
        }
        else
        {
            strategyBLog.info("has enough foes");
            sponsorStages = setUP(aQuest.getStageNum(), theSponsor.getHand());
            for (int i = 0; i < sponsorStages.Length; i++)
            {
                int totalATK = 0;
                for (int j = 0; j < sponsorStages[i].Count; j++)
                {
                    strategyBLog.info(theSponsor.getName() + " put " + sponsorStages[i][j].getName() + " in stage " + (i + 1));
                    if (sponsorStages[i][j].getKind() == Kind.FOE)
                    {
                        FoeCard theFoe = (FoeCard)sponsorStages[i][j];
                        totalATK += theFoe.getAtk();
                        strategyBLog.info("the ATK for this card is " + theFoe.getAtk());
                    }
                    else if (sponsorStages[i][j].getKind() == Kind.WEAPON)
                    {
                        WeaponCard theWeapon = (WeaponCard)sponsorStages[i][j];
                        totalATK += theWeapon.getAtk();
                        strategyBLog.info("the ATK for this card is " + theWeapon.getAtk());
                    }
                    else totalATK = 0;
                }
                strategyBLog.info("total ATK in stage " + (i + 1) + " is " + totalATK);
            }
            for (int i = 0; i < sponsorStages.Length; i++)
            {
                for (int j = 0; j < sponsorStages[i].Count; j++)
                {
                    string cardName = sponsorStages[i][j].getName();
                    Card aCard = theSponsor.getHand().Find(x => x.getName().Contains(cardName));
                    if (aCard != null)
                        theSponsor.getHand().Remove(aCard);
                    else strategyBLog.error("failure to delete the chosen card from sponsor's hand");
                }
            }
        }
        strategyBLog.info(theSponsor.getName() + " has " + theSponsor.getHand().Count + " cards on hand");
        return sponsorStages;
    }

    private bool validFoe(QuestCard aQuestCard, List<Card> sponsorHand)
    {
        //if (aQuestCard.getName().Contains("SFTHG") == true)
        //Debug.Log(aQuestCard.getName());
        int[] miniumATK = { 5, 15, 30, 50 };
        List<Card>[] sponsorStage = new List<Card>[aQuestCard.getStageNum()];
        List<FoeCard> theFoeList = new List<FoeCard>();
        List<FoeCard> sortedFoeList;
        List<WeaponCard> theWeaponList = new List<WeaponCard>();
        List<WeaponCard> sortedWeaponList;
        bool hasTestCard = false;
        int greatestATK;
        int diffCardCount = 0;            //calculate the card's number (not conatin two card has same atk value)
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
                //Debug.Log(theFoe.getName() + "...");
            }
            else if (sponsorHand[i].getKind() == Kind.WEAPON)
            {
                WeaponCard aWeapon = (WeaponCard)sponsorHand[i];
                theWeaponList.Add(aWeapon);
            }
            if (sponsorHand[i].getKind() == Kind.TEST)
                hasTestCard = true;
        }
        sortedFoeList = theFoeList.OrderBy(x => x.getAtk()).ToList();
        sortedWeaponList = theWeaponList.OrderBy(x => x.getAtk()).ToList();
        greatestATK = sortedFoeList[sortedFoeList.Count - 1].getAtk();
        diffCardCount = sortedFoeList.Count;
        for (int i = 1; i < sortedFoeList.Count; i++)
        {
            //Debug.Log(sortedFoeList[i].getName() + " " + sortedFoeList[i].getAtk());
            if (sortedFoeList[i].getAtk() == sortedFoeList[i - 1].getAtk())
            {
                diffCardCount -= 1;
            }
        }
        while (greatestATK < 40)
        {
            if (sortedWeaponList.Count == 0) break;
            greatestATK += sortedWeaponList[0].getAtk();
            sortedWeaponList.Remove(sortedWeaponList[0]);
        }
        if (hasTestCard == true)
        {
            if (aQuestCard.getStageNum() == 2)
            {
                if (greatestATK < 40 || diffCardCount < (aQuestCard.getStageNum() - 1))
                    return false;
                else return true;
            }
            else
            {
                if (greatestATK < 40 || diffCardCount < (aQuestCard.getStageNum() - 1))
                    return false;
                else return true;
            }
        }
        else
        {
            if (greatestATK < 40 || diffCardCount < aQuestCard.getStageNum())
                return false;
            else return true;
        }
    }

    List<Card>[] setUP(int stageNum, List<Card> theHand)
    {
        List<Card>[] theResultList = new List<Card>[stageNum];
        List<FoeCard> sortedList = new List<FoeCard>();
        List<FoeCard> foe = new List<FoeCard>();
        List<WeaponCard> sortedWeaponList = new List<WeaponCard>();
        List<WeaponCard> weapon = new List<WeaponCard>();
        List<Card> stage1 = new List<Card>();
        List<Card> stage2 = new List<Card>();
        List<Card> stage3 = new List<Card>();
        List<Card> stage4 = new List<Card>();
        List<Card> stage5 = new List<Card>();
        List<Card>[] stages = { stage1, stage2, stage3, stage4, stage5 };
        List<Card> other = new List<Card>();
        Card test = new Card();
        bool hasTest = false;
        for (int i = 0; i < theHand.Count; i++)
        {
            if (theHand[i].getKind() == Kind.TEST)
            {
                test = theHand[i];

                hasTest = true;
            }
            if (theHand[i].getKind() == Kind.FOE)
            {
                foe.Add((FoeCard)theHand[i]);
            }
            if (theHand[i].getKind() == Kind.WEAPON)
            {
                weapon.Add((WeaponCard)theHand[i]);
            }
            else
                other.Add(theHand[i]);
        }
        sortedList = foe.OrderBy(x => x.getAtk()).ToList();
        sortedWeaponList = weapon.OrderBy(x => x.getAtk()).ToList();
        if (hasTest == true)
        {
            stages[stageNum - 2].Add(test);
            if (stageNum == 2)
            {
                stages[stageNum - 1].Add(sortedList[sortedList.Count - 1]);
                while (calculateATK(stages[stageNum - 1]) < 40)
                {
                    stages[stageNum - 1].Add(sortedWeaponList[0]);
                    sortedWeaponList.Remove(sortedWeaponList[0]);
                }
                theResultList[0] = stage1;
                theResultList[1] = stage2;
            }
            else
            {
                for (int j = 0; j < sortedList.Count - 1; j++)
                {
                    if (sortedList[j].getAtk() == sortedList[j + 1].getAtk())
                    {
                        sortedList.Remove(sortedList[j]);
                    }
                }
                for (int i = 0; i < stageNum - 2; i++)
                {
                    stages[i].Add(sortedList[0]);
                    sortedList.Remove(sortedList[0]);
                }
                stages[stageNum - 1].Add(sortedList[sortedList.Count - 1]);
                while (calculateATK(stages[stageNum - 1]) < 40)
                {
                    stages[stageNum - 1].Add(sortedWeaponList[0]);
                    sortedWeaponList.Remove(sortedWeaponList[0]);
                }
                for (int i = 0; i < stageNum; i++)
                {
                    theResultList[i] = stages[i];
                }
            }

        }
        else
        {
            for (int j = 0; j < sortedList.Count - 1; j++)
            {
                if (sortedList[j].getAtk() == sortedList[j + 1].getAtk())
                {
                    sortedList.Remove(sortedList[j]);
                }
            }
            for (int i = 0; i < stageNum - 1; i++)
            {
                stages[i].Add(sortedList[0]);
                sortedList.Remove(sortedList[0]);
            }
            stages[stageNum - 1].Add(sortedList[sortedList.Count - 1]);
            while (calculateATK(stages[stageNum - 1]) < 40)
            {
                stages[stageNum - 1].Add(sortedWeaponList[0]);
                sortedWeaponList.Remove(sortedWeaponList[0]);
            }
            for (int i = 0; i < stageNum; i++)
            {
                theResultList[i] = stages[i];
            }
        }
        return theResultList;
    }

    int calculateATK(List<Card> theList)
    {
        int totalATK = 0;
        for (int i = 0; i < theList.Count; i++)
        {
            if (theList[i].getKind() == Kind.FOE)
            {
                FoeCard temp = (FoeCard)theList[i];
                totalATK += temp.getAtk();
            }
            if (theList[i].getKind() == Kind.WEAPON)
            {
                WeaponCard temp = (WeaponCard)theList[i];
                totalATK += temp.getAtk();
            }
        }
        return totalATK;
    }

}


