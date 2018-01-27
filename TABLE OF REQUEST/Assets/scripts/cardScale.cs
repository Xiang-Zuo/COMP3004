using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardScale : MonoBehaviour {

	float currentScale = 1.2f;

	float scaleBack = 0.6f;


	void OnMouseOver()
	{
		//If your mouse hovers over the GameObject with the script attached, output this message
		transform.localScale = new Vector3(currentScale, currentScale, currentScale);
		//this.gameObject.layer = 2;
		Debug.Log("Mouse is over GameObject.");
	}

	void OnMouseExit()
	{
		//The mouse is no longer hovering over the GameObject so output this message each frame
		transform.localScale = new Vector3(scaleBack, scaleBack, scaleBack);
		//this.gameObject.layer = 1;
		Debug.Log("Mouse is no longer on GameObject.");
	}
}
