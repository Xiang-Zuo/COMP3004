using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchShields : MonoBehaviour {

	bool isOn;
	bool isChosen;
	bool p1_choose;
	bool p2_choose;
	bool p3_choose;
	bool display;
	int numOfPlayers;
	GameObject shieldsPanel;
	GameObject blackMask;
	GameObject main_board;
	public GameObject msgBar;
	Text playerIndex;




	// Use this for initialization
	void Start () {
		//main_board.SetActive(false)
		int count = 0; //count player who already chose shield.
		 isOn = false;
		 isChosen = false;
		 p1_choose = false;
		 p2_choose = false;
		 p3_choose = false;
		display = false;
		playerIndex = GameObject.Find ("PlayerIndex").GetComponent<Text> ();
		numOfPlayers = GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().getNumofPlayers();
		shieldsPanel = GameObject.Find ("SelectSheild");
		blackMask = GameObject.Find ("blackmask");
		main_board = GameObject.Find ("MAIN");

		msgBar.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {



	}

	void OnMouseOver(){
		if (!isOn && !p1_choose && !p2_choose && !p3_choose) {
			gameObject.transform.localScale += new Vector3 (0.5f, 0.5f, 0);
			isOn = true;

		}
	}

	void OnMouseExit() {
		if (isOn && !p1_choose && !p2_choose && !p3_choose) {
			gameObject.transform.localScale -= new Vector3 (0.5f, 0.5f, 0);
			isOn = false;
		}
	}


	void OnMouseDown(){
		char piStr = playerIndex.text[0];
		int pi = piStr - '0';



		if (pi== 1 && !isChosen) {//PLAYER index 
			gameObject.transform.localScale += new Vector3 (0.2f, 0.2f, 0);
			p1_choose = true;
			isChosen = true;
			int shieldIndex = -1;
			if (gameObject.name.Length == 9) {
				char si= gameObject.name [8];
				shieldIndex = si - '0';
			} else {
				char si = gameObject.name [9];
				shieldIndex = (si - '0') + 10;
			}

			if (shieldIndex >= 0) {

				GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().setNumofPlayers (1, shieldIndex);
			}
			if (numOfPlayers > 1) {
				playerIndex.text = "2";
			} else {
				shieldsPanel.SetActive (false);
				blackMask.SetActive (false);
				main_board.SetActive (true);
				main_board.GetComponent<InitialGame> ().shieldDisplay ();
				GameObject.Find ("GameController").GetComponent<gameControl> ().activateGame();
				//msgBar.SetActive (true);
			}

		}
		if (pi== 2 && !isChosen) {
			gameObject.transform.localScale += new Vector3 (0.2f, 0.2f, 0);
			p2_choose = true;
			isChosen = true;
			int shieldIndex = -1;
			if (gameObject.name.Length == 9) {
				char si= gameObject.name [8];
				shieldIndex = si - '0';
			} else {
				char si = gameObject.name [9];
				shieldIndex = (si - '0') + 10;
			}

			if (shieldIndex >= 0) {

				GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().setNumofPlayers (2, shieldIndex);
			}
			if (numOfPlayers > 2) {
				playerIndex.text = "3";
			} else {
				shieldsPanel.SetActive (false);
				blackMask.SetActive (false);
				main_board.SetActive (true);
				main_board.GetComponent<InitialGame> ().shieldDisplay ();
				GameObject.Find ("GameController").GetComponent<gameControl> ().activateGame();
				//msgBar.SetActive (true);
			}

		}
		if(pi == 3 && !isChosen) {
			gameObject.transform.localScale += new Vector3 (0.2f, 0.2f, 0);
			p3_choose = true;
			isChosen = true;
			int shieldIndex = -1;
			if (gameObject.name.Length == 9) {
				char si= gameObject.name [8];
				shieldIndex = si - '0';
			} else {
				char si = gameObject.name [name.Length -1];
				shieldIndex = (si - '0') + 10;
			}

			if (shieldIndex >= 0) {

				GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().setNumofPlayers (3, shieldIndex);
			}
			shieldsPanel.SetActive (false);
			blackMask.SetActive (false);
			main_board.SetActive (true);
			main_board.GetComponent<InitialGame> ().shieldDisplay ();
			GameObject.Find ("GameController").GetComponent<gameControl> ().activateGame();
			//msgBar.SetActive (true);
		}

	}

}
