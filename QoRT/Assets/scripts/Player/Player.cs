using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private List<Card> hand;
    private List<Card> allyAndAmour = new List<Card>();
    private List<ShieldCard> shield;
    private int atk = 0;
    private Rank rank;
    private PlayerType type;
    private bool pass;


    public Player(string aName, List<Card> aHand, Rank aRank, PlayerType aType, List<ShieldCard> aShield)
    {
        this.name = aName;
        this.hand = aHand;
        rank = aRank;
        type = aType;
        shield = aShield;
        pass = false;
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string aName)
    {
        this.name = aName;
    }

    public List<Card> getHand()
    {
        return this.hand;
    }

    public void setHand(List<Card> aHand)
    {
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

    public void addAShield(ShieldCard aCard)
    {
        shield.Add(aCard);
    }



    public PlayerType getType()
    {
        return type;
    }

    public void setType(PlayerType newType)
    {
        type = newType;
    }

    public bool getPass()
    {
        return pass;
    }

    public void setPass(bool newPass)
    {
        pass = newPass;
    }

    public Rank getRank()
    {
        return this.rank;
    }

	public void upgradeRank(){
		if (rank == Rank.SQUIRE)
			rank = Rank.KNIGHT;
		else if (rank == Rank.KNIGHT)
			rank = Rank.CHAMPIONKNIGHT;
		else if (rank == Rank.CHAMPIONKNIGHT)
			rank = Rank.KING;	
	}

    public void print()
    {
        foreach (Card card in hand)
        {
            Debug.Log(card.getName());
        }
    }

    public int getAtk()
    {
        int bounsATK = 0;
        for (int i = 0; i < allyAndAmour.Count; i++)
        {
            if (allyAndAmour[i].getKind() == Kind.ALLY)
            {
                AllyCard aAlly = (AllyCard)allyAndAmour[i];
                bounsATK += aAlly.getAtk();
            }
            else if (allyAndAmour[i].getKind() == Kind.AMOUR)
            {
                AmourCard aAlly = (AmourCard)allyAndAmour[i];
                bounsATK += aAlly.getAtk();
            }
            else { }
        }
        if (rank == Rank.SQUIRE)
        {
            atk = 5;
            return atk + bounsATK;
        }
        else if (rank == Rank.KNIGHT)
        {
            atk = 10;
            return atk + bounsATK;
        }
        else if (rank == Rank.CHAMPIONKNIGHT)
        {
            atk = 20;
            return atk + bounsATK;
        }
        else if (rank == Rank.KING)
        {
            atk = 100;
            return atk + bounsATK;
        }
        else
        {
            return -1;
        }
    }

    public void setAllayOrAmour(List<Card> alist)
    {
        allyAndAmour = alist;
    }

    public void addAllayOrAmour(Card aCard)
    {
        allyAndAmour.Add(aCard);
    }

    public List<Card> getAllayOrAmour()
    {
        return allyAndAmour;
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
