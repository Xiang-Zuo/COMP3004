using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

	public class Context
	{
		Strategy strategy;
		public Context(Strategy strategy)
		{
			this.strategy = strategy;
		}

		public void ContextInterface()
		{
			strategy.strategyInterface();

		}
	}


