using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponsorConfirmBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){	
		int whichStage = GameObject.Find ("GameController").GetComponent<gameControl>().getWhichStageNowIs();
        if (GameObject.Find("GameController").GetComponent<gameControl>().sponsorCardsIn(whichStage))
        { 
            GameObject.Find("GameController").GetComponent<gameControl>().toNextStage();
        }
        else
        {
            Player a = GameObject.Find("GameController").GetComponent<gameControl>().sponsorNow;
            GameObject.Find("GameController").GetComponent<gameControl>().sortCard(a);
        }			
	}
}
