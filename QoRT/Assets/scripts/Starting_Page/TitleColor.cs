using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleColor : MonoBehaviour {

	Text title;
	Color aColor;
	float aValue;
	bool stop;
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

	}
	
	// Update is called once per frame
	void Update () {

		aValue = aColor.a;
		if (aValue < 15) {
			aColor.a += 0.05f;
			title.material.color = aColor;

		} else {
			if (gameObject.transform.position.y < 50) {
				gameObject.transform.Translate (Vector3.up * 100 * Time.deltaTime);
			} else {

				if (start.rectTransform.position.y < -250) {
					start.rectTransform.Translate (Vector3.up * 250 * Time.deltaTime);
					guide.rectTransform.Translate (Vector3.up * 250 * Time.deltaTime);
				}
			}


		}
	}
		

}
