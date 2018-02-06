using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//namespace AssemblyCSharp
//{
	public class ConcreteStrategyA:Strategy
	{
		public override void doIParticipateInTournament ()
		{
			Debug.Log("算法A实现");
			//Test.text = "算法A实现";
		}

	}
//}

