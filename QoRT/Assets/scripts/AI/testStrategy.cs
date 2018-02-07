using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TestStrategy : MonoBehaviour{

	void Start(){
		// Use this for initialization
		Context context;
		context = new Context (new ConcreteStrategyA());
		context.ContextInterface();
		context = new Context (new ConcreteStrategyB());
		context.ContextInterface();
	}
}
