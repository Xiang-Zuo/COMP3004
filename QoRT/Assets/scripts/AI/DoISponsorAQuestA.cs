using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class DoISponsorAQuestA: AbstractAI
{

	private QuestGame.Logger strategyALog = new QuestGame.Logger(true);

	public override List<Card>[] DoISponsorAQuest(QuestCard aQuest, Player[] players, Player theSponsor)
	{
		List<Card>[] sponsorStages = new List<Card>[aQuest.getStageNum()];
		//Debug.Log(aQuest.getStageNum());
		strategyALog.info("Now is in AIStrategyA, " + theSponsor.getName() + " is the Sponsor");
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
					strategyALog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
				}
				else if (players[i].getRank() == Rank.KNIGHT && (players[i].getShield().Count + aQuest.getStageNum()) >= 70)
				{
					evolve = true;
					strategyALog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
				}
				else if (players[i].getRank() == Rank.CHAMPIONKNIGHT && (players[i].getShield().Count + aQuest.getStageNum()) >= 100)
				{
					evolve = true;
					strategyALog.info(players[i].getName() + " is possible to evolve if he/she win this quest, AIStrategyB do not sponsor");
				}
				else evolve = false;
			}
			if (evolve == true)
			{
				strategyALog.info("AIStrategyA ends here\n");
				return null;
			}
		}
		//strategyALog.info("start");
		if (validFoe(aQuest, theSponsor.getHand()) == false)
		{
			strategyALog.info("no enough foes");
			return null;
		}else
		{
			strategyALog.info("has enough foes");
			sponsorStages=setUP(aQuest.getStageNum(), theSponsor.getHand());           
			for (int i=0; i < sponsorStages.Length; i++)
			{
				int totalATK = 0;
				for (int j=0; j<sponsorStages[i].Count; j++)
				{
					strategyALog.info(theSponsor.getName()+" put "+sponsorStages[i][j].getName()+" in stage "+(i+1));
					if (sponsorStages[i][j].getKind() == Kind.FOE)
					{
						FoeCard theFoe = (FoeCard)sponsorStages[i][j];
						totalATK += theFoe.getAtk();
						strategyALog.info("the ATK for this card is " + theFoe.getAtk());
					}else if (sponsorStages[i][j].getKind() == Kind.WEAPON)
					{
						WeaponCard theWeapon = (WeaponCard)sponsorStages[i][j];
						totalATK += theWeapon.getAtk();
						strategyALog.info("the ATK for this card is " + theWeapon.getAtk());
					}
					else totalATK = 0;
				}
				strategyALog.info("total ATK in stage " + (i + 1) + " is " + totalATK);
			}
			for(int i=0; i<sponsorStages.Length; i++)
			{
				for (int j=0; j<sponsorStages[i].Count; j++)
				{
					string cardName = sponsorStages[i][j].getName();
					Card aCard = theSponsor.getHand().Find(x=> x.getName().Contains(cardName));
					if (aCard != null)
						theSponsor.getHand().Remove(aCard);
					else strategyALog.error("failure to delete the chosen card from sponsor's hand");
				}
			}
		}
		strategyALog.info(theSponsor.getName() +" has " + theSponsor.getHand().Count+" cards on hand");
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
		int diffCardCount=0;            //calculate the card's number (not conatin two card has same atk value)

		List<WeaponCard> theWeaponList2 = new List<WeaponCard>();
		List<WeaponCard> sortedWeaponList2;
		int horseNum = 0;
		int swordNum = 0;
		int daggerNum = 0;
		int excaliburNum = 0;
		int lanceNum = 0;
		int battleaxNum = 0;

		for (int i = 0; i < sponsorHand.Count; i++) {
			if (sponsorHand [i].getKind () == Kind.WEAPON) {
				if (sponsorHand [i].getName ().Contains ("Horse")) {
					horseNum++;
				} else if (sponsorHand [i].getName ().Contains ("Sword")) {
					swordNum++;
				} else if (sponsorHand [i].getName ().Contains ("Dagger")) {
					daggerNum++;
				} else if (sponsorHand [i].getName ().Contains ("Excalibur")) {
					excaliburNum++;
				} else if (sponsorHand [i].getName ().Contains ("Lance")) {
					lanceNum++;
				} else if (sponsorHand [i].getName ().Contains ("Battleax")) {
					battleaxNum++;
				}

				if (horseNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					horseNum--;
				} else if (swordNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					swordNum--;
				} else if (daggerNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					daggerNum--;
				} else if (excaliburNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					excaliburNum--;
				} else if (lanceNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					lanceNum--;
				} else if (battleaxNum >= 2) {
					theWeaponList2.Add ((WeaponCard)sponsorHand [i]);
					battleaxNum--;
				}
			}
		}

		for (int i = 0; i < sponsorHand.Count; i++)
		{
			if (sponsorHand[i].getKind() == Kind.FOE)
			{
				diffCardCount = 1;
				FoeCard theFoe = (FoeCard)sponsorHand[i];
				theFoe.initialTheATK();
				if (aQuestCard.getName ().Contains ("JTTEF")) {
					if (theFoe.getName ().Contains ("Evil")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				if (aQuestCard.getName ().Contains ("RTSR")) {
					if (theFoe.getName ().Contains ("Saxons")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				if (aQuestCard.getName ().Contains ("BH")) {
					if (theFoe.getName ().Contains ("Boar")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				if (aQuestCard.getName ().Contains ("DTQH")) {
					theFoe.setAtk (theFoe.getAtkSpecial ());
				}
				if (aQuestCard.getName ().Contains ("STD")) {
					if (theFoe.getName ().Contains ("Dragon")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				if (aQuestCard.getName ().Contains ("RTFM")) {
					if (theFoe.getName ().Contains ("Black")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				if (aQuestCard.getName ().Contains ("SFTHG")) {
					theFoe.setAtk (theFoe.getAtkSpecial ());
				}
				if (aQuestCard.getName ().Contains ("TOTGK")) {
					if (theFoe.getName ().Contains ("Green")) {
						theFoe.setAtk (theFoe.getAtkSpecial ());
					}
				}
				else { } 
				theFoeList.Add(theFoe);
				//Debug.Log(theFoe.getName() + "...");
			}else if (sponsorHand[i].getKind() == Kind.WEAPON)
			{
				WeaponCard aWeapon = (WeaponCard)sponsorHand[i];
				theWeaponList.Add(aWeapon);
			}
			if (sponsorHand[i].getKind() == Kind.TEST)
				hasTestCard = true;
		}
		sortedFoeList = theFoeList.OrderBy(x => x.getAtk()).ToList();
		sortedWeaponList=theWeaponList.OrderBy(x => x.getAtk()).ToList();
		sortedWeaponList2=theWeaponList2.OrderBy(x => x.getAtk()).ToList();

		for (int j = sortedFoeList.Count-1; j >0; j--)
		{
			if (sortedFoeList[j].getAtk() == sortedFoeList[j - 1].getAtk())
			{
				sortedFoeList.Remove(sortedFoeList[j]);
			}
		}

		if (sortedWeaponList2.Count != 0) {
			strategyALog.info ("======================" + sortedFoeList [sortedFoeList.Count - 1].getName ());
			strategyALog.info ("======================" + sortedFoeList [sortedFoeList.Count - 1].getAtk ());
			for (int i = 0; i < sortedWeaponList2.Count; i++) {
				strategyALog.info ("======================" + sortedWeaponList2 [i].getName ());
				strategyALog.info ("======================" + sortedWeaponList2 [i].getAtk ());
			}
		} else {
			strategyALog.info ("======================" + sortedFoeList [sortedFoeList.Count - 1].getName ());
			strategyALog.info ("======================" + sortedFoeList [sortedFoeList.Count - 1].getAtk ());
		}

		greatestATK = sortedFoeList[sortedFoeList.Count-1].getAtk();

		while (greatestATK < 50)
		{
			if (sortedWeaponList.Count == 0) break;
			greatestATK += sortedWeaponList[0].getAtk();
			sortedWeaponList.Remove(sortedWeaponList[0]);            
		}

		if(sortedWeaponList2.Count != 0){
			if (sortedWeaponList2 [sortedWeaponList2.Count - 1].getAtk () + sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) {
				strategyALog.info ("======================Weapon + foe < 50");
				return false;
			} else {
				strategyALog.info ("======================Weapon + foe >= 50");
				return true;
			}
		}else{
			if (sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) {
				strategyALog.info ("======================no weapon and foe < 50");
				return false;
			}else{
				strategyALog.info ("======================no weapon and foe > 50");
				return true;
			}
		}

		if (hasTestCard == true)
		{

			if(sortedWeaponList2.Count != 0){
				if ((sortedWeaponList2 [sortedWeaponList2.Count - 1].getAtk () + sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) || (greatestATK < 50 || diffCardCount < (aQuestCard.getStageNum() - 1))) {
					strategyALog.info ("======================Weapon + foe < 50");
					return false;
				} else {
					strategyALog.info ("======================Weapon + foe >= 50");
					return true;
				}
			}else{
				if ((sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) || (greatestATK < 50 || sortedFoeList.Count < (aQuestCard.getStageNum() - 1))) {
					strategyALog.info ("======================no weapon and foe < 50");
					return false;
				}else{
					strategyALog.info ("======================no weapon and foe > 50");
					return true;
				}
			}

		}
		else
		{
			if(sortedWeaponList2.Count != 0){
				if ((sortedWeaponList2 [sortedWeaponList2.Count - 1].getAtk () + sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) || (greatestATK < 50 || diffCardCount < (aQuestCard.getStageNum()))) {
					strategyALog.info ("======================Weapon + foe < 50");
					return false;
				} else {
					strategyALog.info ("======================Weapon + foe >= 50");
					return true;
				}
			}else{
				if ((sortedFoeList [sortedFoeList.Count - 1].getAtk () < 50) || (greatestATK < 50 || sortedFoeList.Count < (aQuestCard.getStageNum()))) {
					strategyALog.info ("======================no weapon and foe < 50");
					return false;
				}else{
					strategyALog.info ("======================no weapon and foe > 50");
					return true;
				}
			}
		}

	}

	List<Card>[] setUP(int stageNum, List<Card> theHand)
	{
		List<Card>[] theResultList = new List<Card>[stageNum];
		List<FoeCard> sortedList=new List<FoeCard>();
		List<FoeCard> foe = new List<FoeCard>();
		List<Card> stage1=new List<Card>();
		List<Card> stage2=new List<Card>();
		List<Card> stage3=new List<Card>();
		List<Card> stage4=new List<Card>();
		List<Card> stage5 = new List<Card>();
		List<Card>[] stages = { stage1, stage2,stage3,stage4,stage5 };
		List<Card> other = new List<Card>();
		Card test=new Card();
		bool hasTest = false;

		List<WeaponCard> theWeaponList2 = new List<WeaponCard>();
		List<WeaponCard> sortedWeaponList2;
		int horseNum = 0;
		int swordNum = 0;
		int daggerNum = 0;
		int excaliburNum = 0;
		int lanceNum = 0;
		int battleaxNum = 0;

		for (int i = 0; i < theHand.Count; i++) {
			if (theHand [i].getKind () == Kind.WEAPON) {
				if (theHand [i].getName ().Contains ("Horse")) {
					horseNum++;
				} else if (theHand [i].getName ().Contains ("Sword")) {
					swordNum++;
				} else if (theHand [i].getName ().Contains ("Dagger")) {
					daggerNum++;
				} else if (theHand [i].getName ().Contains ("Excalibur")) {
					excaliburNum++;
				} else if (theHand [i].getName ().Contains ("Lance")) {
					lanceNum++;
				} else if (theHand [i].getName ().Contains ("Battleax")) {
					battleaxNum++;
				}

				if (horseNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					horseNum--;
				} else if (swordNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					swordNum--;
				} else if (daggerNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					daggerNum--;
				} else if (excaliburNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					excaliburNum--;
				} else if (lanceNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					lanceNum--;
				} else if (battleaxNum >= 2) {
					theWeaponList2.Add ((WeaponCard)theHand [i]);
					battleaxNum--;
				}
			}
		}

		for (int i=0; i < theHand.Count; i++)
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

			else
				other.Add(theHand[i]);
		}
		sortedList = foe.OrderBy(x => x.getAtk()).ToList();
		sortedWeaponList2 = theWeaponList2.OrderBy(x => x.getAtk()).ToList();

		if (hasTest == true)
		{
			stages[stageNum-2].Add(test);
			if (stageNum == 2)
			{
				if (sortedWeaponList2.Count != 0) {
					stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
					sortedList.Remove (sortedList[sortedList.Count-1]);
					stages [stageNum - 1].Add (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
					sortedWeaponList2.Remove (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
				} else {
					stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
					sortedList.Remove (sortedList[sortedList.Count-1]);
				}

				theResultList[0] = stage1;
				theResultList[1] = stage2;
			}
			else
			{
				for (int j = sortedList.Count-1; j >0; j--)
				{
					if (sortedList[j].getAtk() == sortedList[j - 1].getAtk())
					{
						sortedList.Remove(sortedList[j]);
					}
				}

				if (sortedWeaponList2.Count != 0) {
					stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
					sortedList.Remove (sortedList[sortedList.Count-1]);
					stages [stageNum - 1].Add (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
					sortedWeaponList2.Remove (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
				} else {
					stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
					sortedList.Remove (sortedList[sortedList.Count-1]);
				}

				for (int i = stageNum - 3; i >-1; i--) {
					if (sortedWeaponList2.Count > 0) {
						stages [i].Add (sortedList [sortedList.Count - 1]);
						sortedList.Remove (sortedList [sortedList.Count - 1]);
						stages [i].Add (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
						sortedWeaponList2.Remove (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
					} else {
						stages [i].Add (sortedList [sortedList.Count - 1]);
						sortedList.Remove (sortedList [sortedList.Count - 1]);
					}

				}



				for (int i = 0; i < stageNum; i++)
				{
					theResultList[i] = stages[i];
				}
			}

		}else
		{           
			for (int j = sortedList.Count-1; j >0; j--)
			{
				if (sortedList[j].getAtk() == sortedList[j - 1].getAtk())
				{
					sortedList.Remove(sortedList[j]);
				}
			}

			if (sortedWeaponList2.Count != 0) {
				stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
				sortedList.Remove (sortedList[sortedList.Count-1]);
				stages [stageNum - 1].Add (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
				sortedWeaponList2.Remove (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
			} else {
				stages[stageNum - 1].Add(sortedList[sortedList.Count-1]);
				sortedList.Remove (sortedList[sortedList.Count-1]);
			}

			for (int i = stageNum - 2; i >-1; i--) {
				if (sortedWeaponList2.Count > 0) {
					stages [i].Add (sortedList [sortedList.Count - 1]);
					sortedList.Remove (sortedList [sortedList.Count - 1]);
					stages [i].Add (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
					sortedWeaponList2.Remove (sortedWeaponList2 [sortedWeaponList2.Count - 1]);
				} else {
					stages [i].Add (sortedList [sortedList.Count - 1]);
					sortedList.Remove (sortedList [sortedList.Count - 1]);
				}

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
		for (int i=0; i < theList.Count; i++)
		{
			if (theList[i].getKind() == Kind.FOE)
			{
				FoeCard temp = (FoeCard)theList[i];
				totalATK += temp.getAtk();
			}
			if (theList[i].getKind()==Kind.WEAPON)
			{
				WeaponCard temp = (WeaponCard)theList[i];
				totalATK += temp.getAtk();
			}
		}
		return totalATK;
	}

}


