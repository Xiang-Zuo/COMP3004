using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour {

	public GameObject settingMenu;
	public GameObject mask;

	// Use this for initialization
	public GameObject soundEffect;

	void Start(){

	}



	public void closeWindow (){

		settingMenu.SetActive (false);
		mask.SetActive (false);
	}
}
