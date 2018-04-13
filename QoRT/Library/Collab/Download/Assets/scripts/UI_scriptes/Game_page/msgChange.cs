using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class msgChange : MonoBehaviour {
	private string[] msgs;


	public Button greenBtn;
	public Button redBtn;

	public GameObject btns	;

	public Text msg;
	public Text greenText;
	public Text redText;

	// Use this for initialization
	void Start () {
		msgs = new string[10];
		msgs [0] = "Would you like to sponsor this Quest?"; //Quest card showed
		msgs [1] = "Would you like to join this Tournaments?";//Tournament card showed
		msgs [2] = "Waiting for other players make decision...";  //when Sponsor finished
		msgs [3] = "Please place cards on the stage...";//when clicked YES on Quest or Tournament
		msgs [4] = "Please place cards for Tournament...";//After join tournament
		msgs [5] = "Please place your Bids...";//when this stage is a Test card 
		msgs [6] = "Waiting for other players place cards...";//

		msgs [9] = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void setMsg(int index){

		int idx = index;

		switch (idx) 
		{
			
		case 0:
			greenText.text = "YES";
			btns.SetActive(true);
			msg.text = msgs[0];
			break;

		case 1:
			greenText.text = "YES";
			btns.SetActive(true);
			msg.text = msgs[1];
			break;

		case 2:
			btns.SetActive(false);
			msg.text = msgs[2];
			break;

		case 3:
			greenText.text = "PLACE";
			btns.SetActive(true);
			msg.text = msgs[3];
			break;

		case 4:
			greenText.text = "PLACE";
			btns.SetActive(true);
			msg.text = msgs[4];
			break;

		case 5:
			greenText.text = "PLACE";
			btns.SetActive(true);
			msg.text = msgs[5];
			break;

		case 6:
			btns.SetActive(false);
			msg.text = msgs[6];
			break;

		case 9:
			btns.SetActive(false);
			msg.text = msgs[9];
			break;

		default:
			btns.SetActive(false);
			msg.text = msgs [9];
			break;
			

							
				
		}


	}

}
