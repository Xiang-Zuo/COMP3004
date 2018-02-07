using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Deck test = new Deck();
        test.loadCard();
        List<Card> advantureDeck = new List<Card>();
        advantureDeck = test.returnList("advanture");
        foreach(Card card in advantureDeck)
        {
            Debug.Log(card.getName());
        }
        print(advantureDeck.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
