using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

	public class Context
	{
        AbstractAI strategy;
        private int stageNum;
		public Context(AbstractAI strategy, int stageNum)
		{
			this.strategy = strategy;
            this.stageNum = stageNum;
		}

		public List<Card>[] DoISponsorAQuest()
		{
            List<Card>[] sponsorStages;
            Debug.Log("create new strategy, it has " + stageNum + " stages ");
			sponsorStages=strategy.DoISponsorAQuest();
            return sponsorStages;
		}
	}


