using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour {

	public Slider slider_bgm;
	public AudioSource backgroundmusic;


	// Use this for initialization
	void Start () {

		slider_bgm.value = backgroundmusic.volume;
	}
	
	// Update is called once per frame
	void Update () {
		backgroundmusic.volume = slider_bgm.value;
	}
}
