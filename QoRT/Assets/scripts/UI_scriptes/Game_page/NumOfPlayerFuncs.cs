using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfPlayerFuncs : MonoBehaviour {


	public GameObject panel_nop;
	public GameObject mask;
	public GameObject gameScene;
	public GameObject shieldPanel;

	GameObject gameCon;


	int numOfPlayer;

	int p1_shieldsIndex;
	int p2_shieldsIndex;
	int p3_shieldsIndex;


	public void num_Player (int num){
        gameCon = GameObject.Find("GameController");
        if (num == 1){
			print("Start initialize 1 player's game.");
			numOfPlayer = 1;
		}
		else if(num == 2){
			print ("Start initialize 2 players' game.");
			numOfPlayer = 2;
		}
		else if(num == 3){
			print ("Start initialize 3 players' game.");
			numOfPlayer = 3;
		}
        else if (num == 4)
        {
            print("Start initialize 4 players' game.");
            numOfPlayer = 4;
        }
		else {
			print ("It's rigging mode...");
			numOfPlayer = 1;
			gameCon = GameObject.Find ("GameController");
			
		}
        gameCon.GetComponent<gameControl>().setHands_players(num);
    }


	public int getShieldIndex(int playerindex){
		if (playerindex == 1) {
			return p1_shieldsIndex;
		}
		if (playerindex == 2) {
			return p2_shieldsIndex;
		}
		if (playerindex == 3) {
			return p3_shieldsIndex;
		}

		return -1;
	}

	public int getNumofPlayers(){
		return numOfPlayer;
	}

	public void setNumofPlayers(int playerIndex, int shieldIndex){
		if (playerIndex == 1) {
			p1_shieldsIndex = shieldIndex;
		}
		if (playerIndex == 2) {
			p2_shieldsIndex = shieldIndex;
		}
		if (playerIndex == 3) {
			p3_shieldsIndex = shieldIndex;
		}

	}

	public void closeWindow (){
		panel_nop.SetActive (false);
	//	mask.SetActive (false);
		shieldPanel.SetActive (true);
	}
}
