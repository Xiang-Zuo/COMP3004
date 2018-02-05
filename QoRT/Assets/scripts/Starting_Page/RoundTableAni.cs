using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoundTableAni : MonoBehaviour {

	public GameObject roundTable;
	public Text guideBtn;

	void OnMouseOver (){
		
		if (roundTable.transform.localScale.x < 10) {
			roundTable.transform.localScale += new Vector3 (0.25f, 0.25f, 0f);
		}
		roundTable.transform.Rotate(new Vector3(0f,0f,-1.5f));
		guideBtn.fontSize = 28;
	}

	void OnMouseExit (){
		roundTable.transform.localScale = new Vector3 (7.5f, 7.5f, 0);
		guideBtn.fontSize = 25;
	}



}
