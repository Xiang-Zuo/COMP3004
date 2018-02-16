using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeStagesHandler : MonoBehaviour {

	public Text stageONE;
	public Text stageTWO;
	public Text stageTHREE;
	public Text btnTEXT;
	public GameObject blackmask;
	public Text msg;
	public GameObject QUEST;
	int STAGE;
	Button tempBtn;




	// Use this for initialization
	void Start () {
		tempBtn = gameObject.GetComponent<Button> ();
		stageONE.color = Color.yellow;
		stageONE.fontSize = 45;
		STAGE = 1;
	}



	// Update is called once per frame
	void Update () {
		if (STAGE < 4) {
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
			stageONE.fontSize = 30;
			stageTWO.fontSize = 45;
			stageTHREE.fontSize = 30;
			stageONE.color = Color.white;
			stageTWO.color = Color.yellow;
			stageTHREE.color = Color.white;
			msg.text = "Please place the cards for Stage TWO...";
			STAGE = 2;
		}

		if (s == 2) {
			stageONE.fontSize = 30;
			stageTWO.fontSize = 30;
			stageTHREE.fontSize = 45;
			stageONE.color = Color.white;
			stageTWO.color = Color.white;
			stageTHREE.color = Color.yellow;
			btnTEXT.text = "FINISH";
			msg.text = "Please place the cards for Stage THREE...";
			STAGE = 3;
		}

		if (s == 3) {
			QUEST.SetActive (false);
			blackmask.SetActive (false);
			STAGE = 4;
		}
	}

}
