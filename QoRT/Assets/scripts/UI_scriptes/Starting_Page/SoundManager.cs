using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	public GameObject effect;

	void OnMouseOver (){

		effect.SetActive (true);
	}

	void OnMouseExit (){
		effect.SetActive (false);
	}

}
