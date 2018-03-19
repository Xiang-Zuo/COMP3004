using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponsorConfirmBtn : MonoBehaviour {

	public GameObject NEXT_BTN_OBJ;
	public GameObject CONFIRM_BTN_OBJ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseDown(){
		
		int whichStage = GameObject.Find ("GameController").GetComponent<gameControl>().getWhichStageNowIs();
        //print ("Stage NUm HERE  is :     " + whichStage);
        if (GameObject.Find("GameController").GetComponent<gameControl>().sponsorCardsIn(whichStage))
        {
            //	print (whichStage + " stage now ");
            //GameObject.Find ("GameController").GetComponent<gameControl> ().YES_btn.SetActive (true);
            //GameObject.Find("GameController").GetComponent<gameControl>().toNextStage();
			NEXT_BTN_OBJ.SetActive (true);
			CONFIRM_BTN_OBJ.SetActive (false);


        }
        else
        {
            Player a = GameObject.Find("GameController").GetComponent<gameControl>().sponsorNow;
            GameObject.Find("GameController").GetComponent<gameControl>().sortCard(a);
            //print ("bad");
        }




			
	}


}
