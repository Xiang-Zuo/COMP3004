using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoStagesHandler : MonoBehaviour {

	public Text stageONE;
	public Text stageTWO;
	public Text btnTEXT;
	public Text msg;
	public GameObject QUEST;
	int STAGE;
	int stageNow;
	Button tempBtn;
	public Text mainMSG;




	// Use this for initialization
	void Start () {
		tempBtn = gameObject.GetComponent<Button> ();
		stageONE.color = Color.yellow;
		stageONE.fontSize = 45;
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
		gameObject.GetComponent<Text> ().color = Color.yellow;

	}




	void OnMouseDown (){
		clickHandler (STAGE);
	}


	void PutCardsIn(){
		GameObject.Find ("GameController").GetComponent<gameControl>().sponsorCardsIn(1);
	}



	void clickHandler(int s){
		if (s == 1) {
			stageONE.fontSize = 30;
			stageTWO.fontSize = 45;
			stageONE.color = Color.white;
			stageTWO.color = Color.yellow;
			btnTEXT.text = "FINISH";
			msg.text = "Please place the cards for Stage Two...";
			GameObject.Find ("GameController").GetComponent<gameControl>().inWhichStage += 1;
			GameObject.Find ("GameController").GetComponent<gameControl> ().CONFIRM_btn.SetActive (true);
		//	GameObject.Find ("GameController").GetComponent<gameControl> ().
			STAGE = 2;
			//gameObject.SetActive (false);


		}



		if (s == 2) {
			STAGE = 100;
			GameObject.Find ("GameController").GetComponent<gameControl> ().endSponsor ();
			GameObject.Find ("GameController").GetComponent<gameControl> ();
			mainMSG.GetComponent<msgChange> ().setMsg (2);
			Player sponsor = GameObject.Find ("GameController").GetComponent<gameControl> ().sponsorNow;
			GameObject.Find ("GameController").GetComponent<gameControl> ().switchView (sponsor);
			QUEST.SetActive (false);
		}
	}

}
