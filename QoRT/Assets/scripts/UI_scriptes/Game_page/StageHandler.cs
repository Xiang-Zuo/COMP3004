using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageHandler : MonoBehaviour {

	public Text btnNEXT;
	//public GameObject blackmask;
	public Text msg;
	public GameObject QUEST;
	public GameObject NEXT_BTN_OBJ;
	public GameObject CONFIRM_BTN_OBJ;
	int STAGE;
	public Text mainMSG;
	int maxStage;



	// Use this for initialization
	void Start () {
		NEXT_BTN_OBJ.SetActive (false);
		STAGE = 1;
	}



	// Update is called once per frame
	void Update () {
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



		if (s == 1) {
			msg.text = "Please place the cards for Stage TWO...";
			if (s + 1 == maxStage) {
				btnNEXT.text = "FINISH";
			}
			GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
			STAGE = 2;
		}

		if (s == 2) {
			if (s == maxStage) {
				STAGE = 100;
				string questName = GameObject.Find ("GameController").GetComponent<gameControl> ().questNow.getName ();
				GameObject theQ = GameObject.Find (questName);
				theQ.transform.localScale = new Vector3 (1f,1f,0);
				Vector3 aPos = GameObject.Find ("s_2_1").transform.position;
				iTween.MoveTo (theQ,aPos, 1f);
				//hide all cards in stages

				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage THREE...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
				if (s + 1 == maxStage) {
					btnNEXT.text = "FINISH";
				}
		//		GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
				STAGE = 3;
			}
		}


		if (s == 3) {
			if (s == maxStage) {
				STAGE = 100;
				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage FOUR...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
				if (s + 1 == maxStage) {
					btnNEXT.text = "FINISH";
				}
				STAGE = 4;
			}
		}

		if (s == 4) {
			if (s == maxStage) {
				STAGE = 100;

				//hide all cards in stages
				for(int i=0;i<used_adventure_Objs.Count;i++){
					//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
					used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
				}

				GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
				QUEST.SetActive (false);
			} else {
				msg.text = "Please place the cards for Stage FIVE...";
				GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
	//			GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
				if (s + 1 == maxStage) {
					btnNEXT.text = "FINISH";
				}
				STAGE = 5;
			}
		}

		if (s == 5) {
			STAGE = 100;

			//hide all cards in stages
			for(int i=0;i<used_adventure_Objs.Count;i++){
				//iTween.MoveTo (used_adventure_Objs[i], usedAdven_POS, 1f);
				used_adventure_Objs[i].transform.localScale = new Vector3(0, 0, 0);
			}

			GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
			QUEST.SetActive (false);
		}

		//print ("MAX is:  " + maxStage + "AND NOW iS:  " + GameObject.Find ("GameController").GetComponent<gameControl> ().inWhichStage + "AND text IS" + btnNEXT.text );

		NEXT_BTN_OBJ.SetActive (false);
		CONFIRM_BTN_OBJ.SetActive (true);

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
