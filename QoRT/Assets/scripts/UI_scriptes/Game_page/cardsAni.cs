using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardsAni : MonoBehaviour {
	GameObject[] tempcards;
	GameObject[] tempcards2;
	List<GameObject> cards;
	int[] posRecorder;


	// Use this for initialization
	void Start () {
		cards = new List<GameObject> ();
		tempcards = GameObject.FindGameObjectsWithTag ("cardsOnHand");
		for (int c = 0; c < tempcards.Length; c++) {
			cards.Add (tempcards [c]);
		//	oringinalCards.Add (tempcards [c]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//5 units per cards
	public void cardPlay(){
		for(int i =0; i < cards.Count; i++) {
			if (cards [i].GetComponent<TouchCard> ().isChosen ()) {
				print ("Destroy : " + cards [i].name);
				Destroy (cards[i]);
				cards.RemoveAt (i);
				i -= 1;

			}
			print ("Length now is :" + cards.Count);
		}
	}
}