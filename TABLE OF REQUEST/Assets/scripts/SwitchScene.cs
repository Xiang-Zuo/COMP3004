using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	public void loadScene(int i)
	{
		SceneManager.LoadScene (1);
	}

	public void quitApp()
	{
		Application.Quit ();
	}
}
