using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColor : MonoBehaviour {

	void Start(){
		gameObject.GetComponent<Renderer> ().material.color = new Color (1f,1f,1f);
	}


	void OnMouseOver (){
		gameObject.GetComponent<Renderer> ().material.color = new Color (2.5f,2.5f,2.5f);
	}

	void OnMouseExit (){
		gameObject.GetComponent<Renderer> ().material.color = new Color (1f,1f,1f);
	}
}
