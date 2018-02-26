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
		//print ("Stage NUm HERE  is :     " + whichStage);
		if (GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorCardsIn (whichStage)) {
			print (whichStage + " stage now ");
			//GameObject.Find ("GameController").GetComponent<gameControl> ().YES_btn.SetActive (true);
			GameObject.Find ("GameController").GetComponent<gameControl> ().toNextStage ();

		} else
			GameObject.Find ("GameController").GetComponent<gameControl> ().sortSponsor ();
			print ("bad");


			
			
	}


}
