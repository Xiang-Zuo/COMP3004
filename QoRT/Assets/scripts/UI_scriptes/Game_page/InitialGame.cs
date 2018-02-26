using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGame : MonoBehaviour {


	GameObject[] shields_p1 = new GameObject[15];
	GameObject[] shields_p2 = new GameObject[15];
	GameObject[] shields_p3 = new GameObject[15];
	GameObject[] shields_p4 = new GameObject[15];

	List<GameObject> shields1;
	List<GameObject> shields2;
	List<GameObject> shields3;
	List<GameObject> shields4;

	int[] shieldsindexs = new int[3];
	int numOfPlayers;

	// Use this for initialization
	void Start () {
		shields1 = new List<GameObject> ();
		shields2 = new List<GameObject> ();
		shields3 = new List<GameObject> ();
		shields4 = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void shieldDisplay(){
		numOfPlayers = GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().getNumofPlayers();
		shieldsindexs [0] = GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().getShieldIndex (1);
		shieldsindexs [1] = -1;
		shieldsindexs [2] = -1;

		if (numOfPlayers > 1) { //number of players = 2 or 3
			shieldsindexs [1] = GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().getShieldIndex (2);
		} 

		if (numOfPlayers > 2) { //number of players = 3
			shieldsindexs [2] = GameObject.Find ("function holder").GetComponent<NumOfPlayerFuncs> ().getShieldIndex (3);
		}

		shields_p1 = GameObject.FindGameObjectsWithTag ("p1shield");
		shields_p2 = GameObject.FindGameObjectsWithTag ("p2shield");
		shields_p3 = GameObject.FindGameObjectsWithTag ("p3shield");
		shields_p4 = GameObject.FindGameObjectsWithTag ("p4shield");
		for (int c = 0; c < 15; c++) {
			shields1.Add (shields_p1 [c]);
			shields2.Add (shields_p2 [c]);
			shields3.Add (shields_p3 [c]);
			shields4.Add (shields_p4 [c]);
		}






		for (int i = 0; i < shields1.Count; i++) {
			char ind = shields1[i].name[shields1[i].name.Length -1];
			int index = ind - '0';
			if (shields1 [i].name.Length > 2) {
				index += 10;
			}

			if (shieldsindexs [0] == index) {
				//print ("i now is : " + i);
			} else {
				//shieldsindexs [0] -= 1;
				Destroy (shields1[i]);
				//shields1.RemoveAt (i);
				//i -= 1;
			}
		}
		if (numOfPlayers > 1) {
			for (int i = 0; i < shields2.Count; i++) {
				char ind = shields2 [i].name [shields2 [i].name.Length - 1];
				int index = ind - '0';
				if (shields2 [i].name.Length > 2) {
					index += 10;
				}

				if (shieldsindexs [1] == index) {
					//print ("i now is : " + i);
				} else {
					//shieldsindexs [0] -= 1;
					Destroy (shields2 [i]);
					//shields1.RemoveAt (i);
					//i -= 1;
				}
			}
		} else {  //Randomly get 2 shields for player 2 and player 3, if ONLY 1 real player(3 AI).
			int randomshield2 = Random.Range (1, 15);
			int randomshield3 = Random.Range (1, 15);

			while (randomshield2 == shieldsindexs [0]) {
				randomshield2 = Random.Range (1, 15);
			}

			while (randomshield3 == shieldsindexs [0] || randomshield3 == randomshield2) {
				randomshield3 = Random.Range (1, 15);
			}

			for (int i = 0; i < shields2.Count; i++) {
				char ind = shields2[i].name[shields2[i].name.Length -1];
				int index = ind - '0';
				if (shields2 [i].name.Length > 2) {
					index += 10;
				}

				if (randomshield2 == index) {
			
				} else {
					//shieldsindexs [0] -= 1;
					Destroy (shields2[i]);
					//shields1.RemoveAt (i);
					//i -= 1;
				}
			}
			for (int i = 0; i < shields3.Count; i++) {
				char ind = shields3[i].name[shields3[i].name.Length -1];
				int index = ind - '0';
				if (shields3 [i].name.Length > 2) {
					index += 10;
				}

				if (randomshield3 == index) {

				} else {
					//shieldsindexs [0] -= 1;
					Destroy (shields3[i]);
					//shields1.RemoveAt (i);
					//i -= 1;
				}
			}
		}

		if (numOfPlayers > 2) {
			for (int i = 0; i < shields3.Count; i++) {
				char ind = shields3 [i].name [shields3 [i].name.Length - 1];
				int index = ind - '0';
				if (shields3 [i].name.Length > 2) {
					index += 10;
				}

				if (shieldsindexs [2] == index) {
					//print ("i now is : " + i);
				} else {
					//shieldsindexs [0] -= 1;
					Destroy (shields3 [i]);
					//shields1.RemoveAt (i);
					//i -= 1;
				}
			}
		} else if(numOfPlayers == 2) {
			int randomshield3 = Random.Range (1, 15);
			while (randomshield3 == shieldsindexs [0] || randomshield3 == shieldsindexs[1]) {
				randomshield3 = Random.Range (1, 15);
			}
			for (int i = 0; i < shields3.Count; i++) {
				char ind = shields3[i].name[shields3[i].name.Length -1];
				int index = ind - '0';
				if (shields3 [i].name.Length > 2) {
					index += 10;
				}

				if (randomshield3 == index) {

				} else {
					//shieldsindexs [0] -= 1;
					Destroy (shields3[i]);
					//shields1.RemoveAt (i);
					//i -= 1;
				}
			}
		}

		//Randomly get a shield for player 4.
		int randomshields4 = Random.Range (1, 15);  //player 4's shield, it's an AI, So random pick



		while (randomshields4 == shieldsindexs [0] || randomshields4 == shieldsindexs [1] || randomshields4 == shieldsindexs [2]) {
			randomshields4 = Random.Range (1, 15);
		}

	
		for (int i = 0; i < shields4.Count; i++) {
			char ind = shields4[i].name[shields4[i].name.Length -1];
			int index = ind - '0';
			if (shields4 [i].name.Length > 2) {
				index += 10;
			}

			if (randomshields4 == index) {
				//print ("i now is : " + i);
			} else {
				//shieldsindexs [0] -= 1;
				Destroy (shields4[i]);
				//shields1.RemoveAt (i);
				//i -= 1;
			}
		}






	}



}
