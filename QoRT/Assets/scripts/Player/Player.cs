using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
	private string name;
	private List<Card> hand;
    private List<ShieldCard> shield;
    private Rank rank;
    private PlayerType type;
      

	public Player(string aName,List<Card> aHand, Rank aRank, PlayerType aType, List<ShieldCard> aShield){
		this.name = aName;
		this.hand = aHand;
        rank = aRank;
        type = aType;
        shield = aShield;
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

    public List<ShieldCard> getShield()
    {
        return this.shield;
    }

    public void setShield(List<ShieldCard> aShield)
    {
        this.shield = aShield;
    }

    public PlayerType getType()
    {
        return type;
    }

    public void setType(PlayerType newType)
    {
        type = newType;
    }

	public void print(){
		foreach (Card card in hand) {
			Debug.Log (card.getName());
		}
	}

    public int getAtk()
    {
        int atk = -1;
        if (rank == Rank.SQUIRE)
        {
            atk = 5;
            return atk;
        }
        else if (rank == Rank.KNIGHT)
        {
            atk = 10;
            return atk;
        }
        else if (rank == Rank.CHAMPIONKNIGHT)
        {
            atk = 20;
            return atk;
        }
        else if (rank == Rank.KING)
        {
            atk = 100;
            return atk;
        }
        else
        {
            return atk;
        }
    }

    public void draw(Card aCard)
    {
        hand.Add(aCard);
    }

    public void discard(Card aCard)
    {
        hand.Remove(aCard);
    }			
}	
