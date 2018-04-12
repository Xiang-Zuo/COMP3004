using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onlineBtnHandler : MonoBehaviour {

	public GameObject mainPanel;
	public GameObject onlinePanel;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void openOnlinePanel(){
		mainPanel.SetActive (false);
		onlinePanel.SetActive (true);
	}

	public void closeOnlinePanel(){
		
		SceneManager.LoadScene(0);
	}


	void OnMouseDown (){
		mainPanel.SetActive (true);
		onlinePanel.SetActive (false);

	}

}
