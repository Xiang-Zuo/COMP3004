using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_move : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < 265) {
			gameObject.transform.Translate (Vector3.up * 5 * Time.deltaTime);
		} else {
			if (gameObject.transform.position.x < 190) {
				gameObject.transform.Translate (Vector3.right * 5 * Time.deltaTime);
			}
				
		}




	}


	void moveTo(GameObject obj, Vector2 final_pos, float aValue){

		for(int i =0; i < 12; i++){
			float distant = 50 * i;
			while (obj.transform.position.x > final_pos.x) {
				obj.transform.Translate (Vector2.down * 5 * Time.deltaTime);
			}

			while (obj.transform.position.y < (final_pos.y + distant)) {
				obj.transform.Translate (Vector2.right * 2 * Time.deltaTime);
			}
		}

	}

}
