using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableRotate : MonoBehaviour {

	// Use this for initialization
	bool enough;
	public float count;

	void Start () {
		enough = false;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//if (gameObject.transform.rotation > -35){
		if (enough == false) {
			gameObject.transform.Rotate (new Vector3 (0f, 0f, 0.25f));
		}
			
		if (count <= 40) {
			count += 20f * Time.deltaTime;
		} else {
			enough = true;
		}


	//	}
	}
		
}
