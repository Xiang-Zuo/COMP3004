<<<<<<< HEAD
﻿using System.Collections;
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
}
=======
﻿using System.Collections;
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
}
>>>>>>> ad4fc3a58e95020aca45f6c3615f4b0c5d352cac
