using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageHandler : MonoBehaviour {

	public Text btnTEXT;
	public Text btnNEXT;
	//public GameObject blackmask;
	public Text msg;
	public GameObject QUEST;
	int STAGE;
	public Text mainMSG;
	int maxStage;



	// Use this for initialization
	void Start () {
		
		STAGE = 1;
	}



	// Update is called once per frame
	void Update () {
		if (STAGE < 5) {
			//blackmask.SetActive (true);
		}
	}

	void OnMouseOver(){
		gameObject.GetComponent<Text> ().fontSize = 20;
		gameObject.GetComponent<Text> ().color = Color.red;
	}

	void OnMouseExit(){
		gameObject.GetComponent<Text> ().fontSize = 15;
		gameObject.GetComponent<Text> ().color = Color.blue;
	}




	void OnMouseDown (){

		clickHandler (STAGE);


	}


	void clickHandler(int s){
		Vector3 usedAdven_POS = GameObject.Find ("USED_ADVENTURE").transform.position;
		List<GameObject> used_adventure_Objs = GameObject.Find ("GameController").GetComponent<gameControl> ().used_adventure_Objs;
		maxStage = GameObject.Find ("GameController").GetComponent<gameControl> ().numOfStagesNow;
		print (maxStage+"======================s");


		if (s == 1) {
			msg.text = "Please place the cards for Stage TWO...";
			btnNEXT.text = "FINISH";
			GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
			GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
			STAGE = 2;
		}

		if (s == 2) {
			if (maxStage == 2) {
				STAGE = 100;

				string questName = GameObject.Find ("GameController").GetComponent<gameControl> ().questNow.getName ();
				GameObject theQ = GameObject.Find (questName);
				theQ.transform.localScale = new Vector3 (1f,1f,0);
				Vector3 aPos = GameObject.Find ("s_2_1").transform.position;
				iTween.MoveTo (theQ,aPos, 1f);
				print ("BTN msg NOW is:   " + btnTEXT.text + "and  btnNext is: " + btnNEXT.text);
				//hide all cards in stages

				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				mainMSG.GetComponent<msgChange> ().setMsg (2);
				Player sponsor = GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorNow;
				//GameObject.Find ("GameController").GetComponent<gameControl> ().switchView (sponsor);
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage THREE...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
				GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
				STAGE = 3;
			}
		}


		if (s == 3) {
			if (maxStage == 3) {
				STAGE = 100;
				btnNEXT.text = "FINISH";

				//hide all cards in stages
				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				mainMSG.GetComponent<msgChange> ().setMsg (2);
				Player sponsor = GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorNow;
				GameObject.Find ("GameController").GetComponent<gameControl> ().switchView (sponsor);
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage FOUR...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
				GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
				STAGE = 4;
			}
		}

		if (s == 4) {
			if (maxStage == 4) {
				STAGE = 100;
				btnNEXT.text = "FINISH";

				//hide all cards in stages
				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				mainMSG.GetComponent<msgChange> ().setMsg (2);
				Player sponsor = GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorNow;
				GameObject.Find ("GameController").GetComponent<gameControl> ().switchView (sponsor);
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage FIVE...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
				GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
				STAGE = 5;
			}
		}

		if (s == 5) {
			STAGE = 100;
			btnNEXT.text = "FINISH";

			//hide all cards in stages
			for(int i=0;i<used_adventure_Objs.Count;i++){
				//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
				used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
			}

			GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
			mainMSG.GetComponent<msgChange> ().setMsg (2);
			Player sponsor = GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorNow;
			GameObject.Find ("GameController").GetComponent<gameControl> ().switchView (sponsor);
			QUEST.SetActive (false);
		}
	}

	/*
	public Vector3 placeSponsorInRightPos(int whichStage){
		string posName = "s_";
		posName += maxStage;
		posName += "_";
		posName += whichStage;
		return (GameObject.Find (posName).GetComponent<Transform> ().position);
	}
	*/

}
