using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class DoISponsorAQuestA : AbstractAI
{
	private QuestGame.Logger strategyALog = new QuestGame.Logger(true);


	public override List<Card>[] DoISponsorAQuest(QuestCard aQuest, Player[] players, Player theSponsor)
	{	/*
		//store weapon card
		List<WeaponCard> theWeaponList = new List<WeaponCard> ();
		List<WeaponCard> sortedWeaponList;
		//store foe card
		List<FoeCard> theFoeList = new List<FoeCard> ();
		List<FoeCard> sortedFoeList;
		//store test card
		List<TestCard> theTestList = new List<TestCard> ();
		//check has testcard or not
		bool hasTestCard = false;
		List<Card>[] sponsorStages = new List<Card>[aQuest.getStageNum ()];
		List<Card> sponsorHand = theSponsor.getHand ();
		*/
		//Debug.Log(aQuest.getStageNum());
		List<Card>[] sponsorStages = new List<Card>[aQuest.getStageNum()];
		strategyALog.info ("Now is in AIStrategyA, " + theSponsor.getName () + " is the Sponsor");
		bool evolve = false;
		for (int i = 0; i < players.Length; i++) {
			if (players [i].getName () == theSponsor.getName ()) {
			} else {
				if (players [i].getRank () == Rank.SQUIRE && (players [i].getShield ().Count + aQuest.getStageNum ()) >= 50) {
					evolve = true;
					strategyALog.info (players [i].getName () + " is possible to evolve if he/she win this quest, AIStrategyA do not sponsor");
				} else if (players [i].getRank () == Rank.KNIGHT && (players [i].getShield ().Count + aQuest.getStageNum ()) >= 70) {
					evolve = true;
					strategyALog.info (players [i].getName () + " is possible to evolve if he/she win this quest, AIStrategyA do not sponsor");
				} else if (players [i].getRank () == Rank.CHAMPIONKNIGHT && (players [i].getShield ().Count + aQuest.getStageNum ()) >= 100) {
					evolve = true;
					strategyALog.info (players [i].getName () + " is possible to evolve if he/she win this quest, AIStrategyA do not sponsor");
				} else
					evolve = false;
			}
			if (evolve == true) {
				strategyALog.info ("AIStrategyA ends here\n");
				return null;
			}
		}
		/*

		//player to be sponsor
		if (evolve == false) {

			//put all weapon card, test card and foe card into different list
			for (int i = 0; i < sponsorHand.Count; i++)
			{
				if (sponsorHand [i].getKind () == Kind.WEAPON) {
					WeaponCard weapon = (WeaponCard)sponsorHand [i];
					theWeaponList.Add (weapon);
				}

				if(sponsorHand [i].getKind () == Kind.FOE){
					FoeCard theFoe = (FoeCard)sponsorHand[i];
					theFoe.initialTheATK();
					if (aQuest.getName().Contains("JTTEF"))
					if (theFoe.getName().Contains("Evil"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("RTSR"))
					if (theFoe.getName().Contains("Saxons"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("BH"))
					if (theFoe.getName().Contains("Boar"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("DTQH"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("STD"))
					if (theFoe.getName().Contains("Dragon"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("RTFM"))
					if (theFoe.getName().Contains("Black"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					if (aQuest.getName().Contains("SFTHG"))
						theFoe.setAtk(theFoe.getAtkSpecial()); 
					if (aQuest.getName().Contains("TOTGK"))
					if (theFoe.getName().Contains("Green"))
						theFoe.setAtk(theFoe.getAtkSpecial());
					else { } 
					theFoeList.Add(theFoe);
				}
				sortedFoeList = theFoeList.OrderBy(x => x.getAtk()).ToList();

				if (sponsorHand [i].getKind () == Kind.TEST) {
					TestCard test = (TestCard)sponsorHand [i];
					hasTestCard = true;
					theTestList.Add (test);
				}
			}

			//if sponsor has a test card
			if (hasTestCard == true) {
				//set up second last stage to be a test
				//sponsorStages [aQuest.getStageNum () - 2] = getRandomTestCard (theTestList);

			} else {
				//set up previous stage to the strongest foe
				//for(int i=sortedFoeList.Count - 1;i>-1;i--){
					
				//}
				for (int i = 0; i < aQuest.getStageNum (); i++) {
					sponsorStages [i].Add(sortedFoeList [sortedFoeList.Count - i]);
				}
			
			}


		}

		return sponsorStages;
	}


		Card getRandomTestCard(List<TestCard> theTestList)
		{
			int randomCard = Random.Range(0, theTestList.Count);
			Card firstCard = theTestList[randomCard];
			return firstCard;
		}
		*/
		if (validFoeAndWeapon(aQuest, theSponsor.getHand()) == false)
		{
			strategyALog.info("no enough foes");
			return null;
		}else
		{
			strategyALog.info("has enough foes");
			
		}



		return sponsorStages;
	}

	private bool validFoeAndWeapon(QuestCard aQuestCard, List<Card> sponsorHand)
	{
		List<WeaponCard> theWeaponList = new List<WeaponCard> ();
		List<WeaponCard> sortedWeaponList;
		//if (aQuestCard.getName().Contains("SFTHG") == true) Debug.Log("ffffffff");
		//Debug.Log(aQuestCard.getName());
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
				if (sponsorHand [i].getKind () == Kind.WEAPON) {
					WeaponCard weapon = (WeaponCard)sponsorHand [i];
					theWeaponList.Add (weapon);
				}
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

		for (int i=0; i < theWeaponList.Count; i++)
		{
		Debug.Log(theWeaponList[i].getName() + " " + theWeaponList[i].getAtk());
		if (theWeaponList[i].getAtk()!= theWeaponList[i - 1].getAtk())
			{
			totalATK += theWeaponList[i].getAtk();
			}
		}

		if (hasTestCard == true)
		{
			if (aQuestCard.getStageNum() == 2)
			{
				if (totalATK < 50 || diffCardCount < (aQuestCard.getStageNum() - 1))
					return false;
				else return true;
			}
			else
			{
				if (totalATK < (50 + miniumATK[aQuestCard.getStageNum() - 3]) || diffCardCount < (aQuestCard.getStageNum() - 1))
					return false;
				else return true;
			}
		}
		else
		{
			if (totalATK < (50 + miniumATK[aQuestCard.getStageNum() - 2]) || diffCardCount < aQuestCard.getStageNum())
				return false;
			else return true;
		}     
	}

	List<Card>[] putInStage(){
		return null;
	}
	
	
	
}