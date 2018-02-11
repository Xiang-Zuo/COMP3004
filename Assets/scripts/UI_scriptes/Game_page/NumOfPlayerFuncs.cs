using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfPlayerFuncs : MonoBehaviour {


	public GameObject panel_nop;
	public GameObject mask;
	public GameObject gameScene;


	public void num_Player (int num){
		if(num == 1){
			print("Start initialize 1 player's game.");
		}
		if(num == 2){
			print ("Start initialize 2 players' game.");
		}
		if(num == 3){
			print ("Start initialize 3 players' game.");
		}
		if(num == 4){
			print ("Start initialize 4 players' game.");
		}

		gameScene.SetActive (true);
	}



	public void closeWindow (){
		panel_nop.SetActive (false);
		mask.SetActive (false);
	}
}
