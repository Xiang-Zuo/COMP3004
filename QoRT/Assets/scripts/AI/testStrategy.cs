using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//namespace AssemblyCSharp
//{
public class testStrategy : MonoBehaviour{

	void Start(){
		// Use this for initialization
		Context context;
		context = new Context (new ConcreteStrategyA());
		//context = new Context (new ConcreteStrategyA());
		context.ContextInterface();
		context = new Context (new ConcreteStrategyB());
		context.ContextInterface();
	}
}
//}

/*abstract public class Strategy
{
	public abstract void doIParticipateInTournament();

}

public class ConcreteStrategyA:Strategy
{
	public override void doIParticipateInTournament ()
	{
		Debug.Log("算法A实现");
		//Test.text = "算法A实现";
	}

}
public class ConcreteStrategyB:Strategy
{
	public override void doIParticipateInTournament ()
	{
		Debug.Log("算法B实现");
		//Test.text = "算法B实现";
	}

}


public class Context
{
	Strategy strategy;
	public Context(Strategy strategy)
	{
		this.strategy = strategy;
	}

	public void ContextInterface()
	{
		strategy.doIParticipateInTournament ();

	}
}
*/