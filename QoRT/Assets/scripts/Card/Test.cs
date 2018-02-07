using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Deck test = new Deck();
        test.loadCard();
        List<Card> advantureDeck = new List<Card>();
        List<Card> storyDeck = new List<Card>();
        List<Card> rankDeck = new List<Card>();
        advantureDeck = test.returnList("advanture");
        storyDeck = test.returnList("story");
        rankDeck = test.returnList("rank");
        foreach (Card card in advantureDeck)
        {
            Debug.Log(card.getName());
        }
        print(advantureDeck.Count);
        print(storyDeck.Count);
        print(rankDeck.Count);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
