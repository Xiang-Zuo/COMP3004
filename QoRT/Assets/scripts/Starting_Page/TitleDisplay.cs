using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleDisplay : MonoBehaviour {


	public Text start;
	public Text exit;
	Text title;
	float count;

	// Use this for initialization
	void Start () {
		start.enabled = false;
	//	exit.enabled = false;
		count = 0;
		//title = gameObject.GetComponent (Text) as Text;
		//title.color.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.localPosition.x <= -130) {
			gameObject.transform.Translate (Vector3.right * 300 * Time.deltaTime);
		} else {
			if (count <= 3) {
				count += 5 * Time.deltaTime;
			} else {
				start.enabled = true;
				exit.enabled = true;
			}
		}

	//	if(title.color.a <= 250){
	//		title.color.a++;
	//	}
	}
}
