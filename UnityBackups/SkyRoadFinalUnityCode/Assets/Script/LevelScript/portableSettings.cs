using UnityEngine;
using System.Collections;

public class portableSettings : MonoBehaviour {
	public UIToggle fxToggle;
	public UIToggle musicToggle;

	// Use this for initialization
	void Start () {
	}

	public void fxEnabler(){
		Debug.Log("PS portableSettings fxEnabler()");
		PlayerPrefs.SetInt ("audioChanged",1);
		PlayerPrefs.SetInt ("FXChanged",1);
	}

	public void MusicEnabler(){
		Debug.Log("PS portableSettings MusicEnabler()");
		PlayerPrefs.SetInt ("audioChanged",1);
		PlayerPrefs.SetInt ("musicChanged",1);
	}
}
