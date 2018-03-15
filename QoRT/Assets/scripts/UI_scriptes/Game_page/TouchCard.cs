using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCard : MonoBehaviour {

	bool isOn;
	bool isUp;
	int sortOrder;
	bool act;
	bool chosenOnS1;
	bool chosenOnS2;
	bool chosenOnS3;
	bool chosenOnS4;
	bool chosenOnS5;
	Transform p1_cardPos;
	Transform p2_cardPos;
	Transform p3_cardPos;
	Transform p4_cardPos;



	// Use this for initialization
	void Start () {
		isOn = false;
		isUp = false;
		act = false;
		chosenOnS1 = false;
		chosenOnS2 = false;
		chosenOnS3 = false;
		chosenOnS4 = false;
		chosenOnS5 = false;
//		p1_cardPos = GameObject.Find ("UIROOT/MAIN/Objects/RankHolderP1/p1cardPos").GetComponent<Transform> ();
	//	p2_cardPos = GameObject.Find ("UIROOT/MAIN/Objects/RankHolderP2/p2cardPos");
//		p3_cardPos = GameObject.Find ("UIROOT/MAIN/Objects/RankHolderP3/p3cardPos");
//		p4_cardPos = GameObject.Find ("UIROOT/MAIN/Objects/RankHolderP4/p4cardPos");

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseOver () {
		if (!isOn & !isUp) {
			//gameObject.transform.Translate (Vector3.up * 1);
			gameObject.transform.localScale += new Vector3 (0.2f, 0.2f, 0);
			sortOrder = gameObject.GetComponent<Renderer> ().sortingOrder;
			gameObject.GetComponent<Renderer> ().sortingOrder = 4;
			isOn = true;
		}

	}

	public void OnMouseExit () {
		if (!isUp) {
			//gameObject.transform.Translate (Vector3.down * 1);
			gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
			gameObject.GetComponent<Renderer> ().sortingOrder = sortOrder;
			isOn = false;
		}
	}


	void OnMouseDown (){
		if (act) {
			if (!isUp) {
				gameObject.transform.localScale += new Vector3(0.05f,0.05f,0);
				gameObject.transform.Translate (Vector3.up * 0.5f);
				gameObject.GetComponent<Renderer> ().sortingOrder = sortOrder;
				isUp = true;
			} else {
				//gameObject.transform.Translate (Vector3.down * 0.5f);
				gameObject.transform.Translate (Vector3.down * 0.5f);
				gameObject.transform.localScale -= new Vector3(0.05f,0.05f,0);
				isUp = false;
			}
		}
	}

	void OnMouseUp(){
		
	}


	public bool isChosen(){
		return isUp;
	}


	public void activateCard(){
		act = true;
	}
	public void stopActivateCard(){
		//act = false;
		isUp = false;
	}

}
