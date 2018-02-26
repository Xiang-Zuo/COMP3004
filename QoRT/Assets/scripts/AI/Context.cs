using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

	public class Context
	{
        AbstractAI strategy;
        QuestCard aQuest;
        Player[] players;
        Player theSponsor;
        
		public Context(AbstractAI strategy, QuestCard aQuest, Player[] players, Player theSponsor)
		{
			this.strategy = strategy;
        	this.aQuest = aQuest;
        	this.players = players;
        	this.theSponsor = theSponsor;
            
		}

		public List<Card>[] DoISponsorAQuest()
		{
            List<Card>[] sponsorStages;
			sponsorStages=strategy.DoISponsorAQuest(aQuest,players,theSponsor);
            return sponsorStages;
		}
	}


