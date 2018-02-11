using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
	private string name;
	private List<Card> hand;

	public Player(string aName,List<Card> aHand){
		this.name = aName;
		this.hand = aHand;
	}

	public string getName(){
		return this.name;
	}

	public void setName(string aName){
		this.name = aName;
	}

	public List<Card> getHand(){
		return this.hand;
	}

	public void setHand(List<Card> aHand){
		this.hand = aHand;
	}

	public void print(){
		foreach (Card card in hand) {
			Debug.Log (card.getName());
		}
	}
}	
