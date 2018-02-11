using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwordAni : MonoBehaviour {

	public GameObject sword;
	public Text startBtn;


	void OnMouseOver (){
		if (sword.transform.localScale.x < 15) {
			sword.transform.localScale += new Vector3 (0.5f, 0.5f, 0);
		}
		startBtn.fontSize = 40;
	}

	void OnMouseExit (){
		sword.transform.localScale = new Vector3 (12f, 12f, 0);
		startBtn.fontSize = 30;
	}
		

}
