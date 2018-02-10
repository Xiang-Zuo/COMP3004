using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

	public class Context
	{
		AIStrategy strategy;
		public Context(AIStrategy strategy)
		{
			this.strategy = strategy;
		}

		public void ContextInterface()
		{
			strategy.strategyInterface();

		}
	}


