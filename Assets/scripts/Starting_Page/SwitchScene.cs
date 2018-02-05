using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
	public GameObject sword;
	bool scaleNow;

	void Start () {
		scaleNow = false;
	}


	void Update (){

		if (scaleNow) {
			sword.GetComponent<Renderer> ().material.color = new Color (2.5f,2.5f,2.5f);
			sword.transform.localScale += new Vector3 (40f, 40f, 0f);
			sword.transform.Translate (Vector3.right * 250 * Time.deltaTime);
			sword.transform.Translate (Vector3.up * 50 * Time.deltaTime);
			sword.transform.Translate (Vector3.forward * 2 * Time.deltaTime);
		}
	}



	IEnumerator toSceneOne(){
		yield return new WaitForSeconds (0.15f);
		SceneManager.LoadScene (1);
	}

	public void loadScene()
	{
		sword.GetComponent<Renderer> ().sortingOrder = 10;
		scaleNow = true;
		StartCoroutine (toSceneOne ());
	}







	public void quitApp()
	{
		Application.Quit ();
	}
}
