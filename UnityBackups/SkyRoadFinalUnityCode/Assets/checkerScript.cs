using UnityEngine;
using System.Collections;

public class checkerScript : MonoBehaviour {
	public GameObject helpWindow;
	public GameObject aboutWindow;
	// Use this for initialization
	public void checkHelpWindow(){
		if(helpWindow.activeSelf){
			helpWindow.SetActive(!helpWindow.activeSelf);
		}
	}
	public void checkAboutWindow(){
		if(aboutWindow.activeSelf){
			aboutWindow.SetActive(!aboutWindow.activeSelf);
		}
	}
}
