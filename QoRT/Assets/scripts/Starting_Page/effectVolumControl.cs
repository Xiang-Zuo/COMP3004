using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectVolumControl : MonoBehaviour {


		public Slider slider_effect;
		public AudioSource sounde;
		public AudioSource sounde1;
		public AudioSource sounde2;
		public AudioSource sounde3;

		// Use this for initialization
		void Start () {
			sounde.volume = 0.4f;
			sounde1.volume = 0.4f;
			sounde2.volume = 0.4f;
			sounde3.volume = 0.4f;
			slider_effect.value = sounde1.volume;
		}

		// Update is called once per frame
		void Update () {
			sounde.volume = slider_effect.value;
			sounde1.volume = slider_effect.value;
			sounde2.volume = slider_effect.value;
			sounde3.volume = slider_effect.value;
		}
}
