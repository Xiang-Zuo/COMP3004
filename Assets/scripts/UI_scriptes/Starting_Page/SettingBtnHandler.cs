using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtnHandler : MonoBehaviour {

	public GameObject settingMenu;
	public GameObject mask;

	// Use this for initialization
	public GameObject effect;

	void Start(){
		settingMenu.SetActive (false);
		mask.SetActive (false);
	}

	void OnMouseOver (){

		effect.SetActive (true);
	}

	void OnMouseExit (){
		effect.SetActive (false);
	}




	void OnMouseDown (){
		
		settingMenu.SetActive (true);
		mask.SetActive (true);
	}
}
