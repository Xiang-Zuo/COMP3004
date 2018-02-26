using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiveStagesHandler : MonoBehaviour {


	public Text stageONE;
	public Text stageTWO;
	public Text stageTHREE;
	public Text stageFOUR;
	public Text stageFIVE;
	public Text btnTEXT;
	public GameObject blackmask;
	public Text msg;
	public GameObject QUEST;
	int STAGE;
	Button tempBtn;
	public Text mainMSG;



	// Use this for initialization
	void Start () {
		tempBtn = gameObject.GetComponent<Button> ();
		stageONE.color = Color.yellow;
		stageONE.fontSize = 30;
		STAGE = 1;
	}



	// Update is called once per frame
	void Update () {
		if (STAGE < 5) {
			blackmask.SetActive (true);
		}
	}

	void OnMouseOver(){
		gameObject.GetComponent<Text> ().fontSize = 25;
		gameObject.GetComponent<Text> ().color = Color.red;
	}

	void OnMouseExit(){
		gameObject.GetComponent<Text> ().fontSize = 20;
		gameObject.GetComponent<Text> ().color = Color.yellow;
	}




	void OnMouseDown (){

		clickHandler (STAGE);


	}


	void clickHandler(int s){
		if (s == 1) {
			stageONE.fontSize = 25;
			stageTWO.fontSize = 30;
			stageTHREE.fontSize = 25;
			stageFOUR.fontSize = 25;
			stageFIVE.fontSize = 25;
			stageFIVE.color = Color.white;
			stageFOUR.color = Color.white;
			stageONE.color = Color.white;
			stageTWO.color = Color.yellow;
			stageTHREE.color = Color.white;
			msg.text = "Please place the cards for Stage TWO...";
			STAGE = 2;
		}

		if (s == 2) {
			stageONE.fontSize = 25;
			stageTWO.fontSize = 25;
			stageTHREE.fontSize = 30;
			stageFOUR.fontSize = 25;
			stageFIVE.fontSize = 25;
			stageFIVE.color = Color.white;
			stageFOUR.color = Color.white;
			stageONE.color = Color.white;
			stageTWO.color = Color.white;
			stageTHREE.color = Color.yellow;
			msg.text = "Please place the cards for Stage THREE...";
			STAGE = 3;
		}


		if (s == 3) {
			stageONE.fontSize = 25;
			stageTWO.fontSize = 25;
			stageTHREE.fontSize = 25;
			stageFOUR.fontSize = 30;
			stageFIVE.fontSize = 25;
			stageFIVE.color = Color.white;
			stageFOUR.color = Color.yellow;
			stageONE.color = Color.white;
			stageTWO.color = Color.white;
			stageTHREE.color = Color.white;
			msg.text = "Please place the cards for Stage FOUR...";
			STAGE = 4;
		}

		if (s == 4) {
			stageONE.fontSize = 25;
			stageTWO.fontSize = 25;
			stageTHREE.fontSize = 25;
			stageFOUR.fontSize = 25;
			stageFIVE.fontSize = 30;
			stageFIVE.color = Color.yellow;
			stageFOUR.color = Color.white;
			stageONE.color = Color.white;
			stageTWO.color = Color.white;
			stageTHREE.color = Color.white;
			btnTEXT.text = "FINISH";
			msg.text = "Please place the cards for Stage FIVE...";
			STAGE = 5;
		}

		if (s == 5) {
			QUEST.SetActive (false);
			blackmask.SetActive (false);
			mainMSG.GetComponent<msgChange> ().setMsg (2);
			STAGE = 5;
		}
	}
}
