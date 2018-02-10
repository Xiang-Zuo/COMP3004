using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCard : MonoBehaviour {

	bool isOn;
	int sortOrder;


	// Use this for initialization
	void Start () {
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseOver () {
		if (!isOn) {
			gameObject.transform.Translate (Vector3.up * 3);
			gameObject.transform.localScale += new Vector3 (0.8f, 0.8f, 0);
			sortOrder = gameObject.GetComponent<Renderer> ().sortingOrder;
			gameObject.GetComponent<Renderer> ().sortingOrder = 10;
			isOn = true;
		}

	}

	void OnMouseExit () {
		gameObject.transform.Translate (Vector3.down * 3);
		gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		gameObject.GetComponent<Renderer> ().sortingOrder = sortOrder;
		isOn = false;
	}

}
