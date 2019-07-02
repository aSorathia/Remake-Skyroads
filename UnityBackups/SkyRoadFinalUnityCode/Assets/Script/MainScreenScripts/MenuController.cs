using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	public TweenPosition tweenPositionPlay;
	public TweenPosition tweenPositionDisable;
	public GameObject helpWindow;
	public GameObject aboutWindow;
	public bool buttonClicked = false;
	public bool tComplete = false;
	public bool reverse = false;
	public void PlayComponent()
	{
		tweenPositionPlay.enabled = true;
	}
	public void StopComponent()
	{
		tweenPositionDisable.enabled=false;
	}
	public void DisableControl(bool state){
		Debug.Log (state);
		NGUITools.SetActiveChildren(gameObject, state);
	}
	public void playClicked(){
		buttonClicked = true;
	}
	public void titleComplete(){
		if(buttonClicked){
			Debug.Log ("proper");
			Application.LoadLevel("LevelManager");
		}else{
			Debug.Log ("Not Debug.Log proper");
		}
	}
	public void displayHelp(){
		helpWindow.SetActive(true);
	}
	public void closeHelp(){
		helpWindow.SetActive(false);
	}
	public void displayAbout(){
		aboutWindow.SetActive(true);
	}
	public void closeAbout(){
		aboutWindow.SetActive(false);
	}
}
