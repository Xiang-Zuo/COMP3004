using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
	public GameObject sword;
	public GameObject startSound;
	bool scaleSwordNow;


	void Start () {
		scaleSwordNow = false;
	}


	void Update (){

		if (scaleSwordNow) {
			sword.GetComponent<Renderer> ().material.color = new Color (2.5f,2.5f,2.5f);
			sword.transform.localScale += new Vector3 (30f, 30f, 0f);
			sword.transform.Translate (Vector3.right * 500 * Time.deltaTime);
			sword.transform.Translate (Vector3.up * 50 * Time.deltaTime);
			sword.transform.Translate (Vector3.forward * 2 * Time.deltaTime);
		}
	}



	IEnumerator toSceneOne(){
		yield return new WaitForSeconds (0.8f);
		SceneManager.LoadScene (1);
	}

	public void loadScene()
	{
		startSound.SetActive (true);
		sword.GetComponent<Renderer> ().sortingOrder = 10;
		scaleSwordNow = true;
		StartCoroutine (toSceneOne ());
	}







	public void quitApp()
	{
		Application.Quit ();
	}
}
