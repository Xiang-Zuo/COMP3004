using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class guideManager : MonoBehaviour {

	int stat;
	public Text title;
	public Text content;
	public Text btnTXT;
	// Use this for initialization
	void Start () {
		stat = 0;
		content.fontSize = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void guideHandler(){
		if (stat == 0) {
			title.text = "";
			content.text = "Earn shields from stroies!";
			content.fontSize = 50;
			stat = 1;
			return;
		}
		if (stat == 1) {
			title.text = "Adventure Deck";
			content.text = "Includes Foe cards, Weapon cards, Ally card, Amour Cards and Test Cards.";
			content.fontSize = 20;
			stat = 2;
			return;
		}
		if (stat == 2) {
			title.text = "Rank Deck";
			content.text = "Use 5 shields to upgrade from Squire to Knight\n\n\nUse 7 shields to upgrade from Knight to Champion Knight\n\n\nUse 10 shields to upgrade from Champion Knight to the Knight of the Round Table!";
			content.fontSize = 20;
			stat = 3;
			return;
		}
		if (stat == 3) {
			title.text = "";
			content.text = "Become the Knight of Round Table to win the game!!";
			content.fontSize = 50;
			btnTXT.text = "play";
			stat = 4;
			return;
		}
		if (stat == 4) {
			SceneManager.LoadScene (0);
			return;
		}
	}


}
