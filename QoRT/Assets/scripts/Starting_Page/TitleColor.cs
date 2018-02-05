using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleColor : MonoBehaviour {

	Text title;
	Color aColor;
	float aValue;
	public Text start;
	public Text guide;
	public GameObject sword;
	public GameObject roundTable;

	// Use this for initialization
	void Start () {
		title = gameObject.GetComponent<UnityEngine.UI.Text>();
		aColor = title.material.color;
		aColor.a = 5f;
		title.material.color = aColor;
		start.enabled = false;
		guide.enabled = false;
		sword.SetActive (false);
		roundTable.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		print (aValue);
		aValue = aColor.a;
		if (aValue < 15) {
			aColor.a += 0.05f;
			title.material.color = aColor;

		} else {
			start.enabled = true;
			guide.enabled = true;
			sword.SetActive (true);
			roundTable.SetActive (true);
		}
	}
}
