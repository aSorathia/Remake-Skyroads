using UnityEngine;
using System.Collections;

public class StageSetter : MonoBehaviour {

	public string Levelname="";
	public UISprite[] ratingSprite;
	public int ratingValue=0;
	public UILabel Time;
	public UILabel Deaths;
	public UIToggle Completed;
	public UIToggle Key;
	public string time="";
	public string deaths="";
	public bool completed=false;
	public bool key=false;
	//public GameObject LevelPrefab;
	//public Transform positionPrefab;
	public UIToggle toggle;
	public UILabel ScoreLabel;
	public string Score;

	// Use this for initialization
	public void Awake () {
		Debug.Log ("called1");
		Time.text = "0";
		Deaths.text = "0";
		Key.value = false;
		Completed.value = false;
		ScoreLabel.text = "0";

		//To initilize Rating Stats
		for (int i=1; i<=5; i++) {
			ratingSprite[i].enabled=false;	
		}
		if (ratingValue != 0) {
			ratingSprite [ratingValue].enabled = true;		
		} else {
			//Debug.Log ("0 Rating-StageStter");
		}
		//Sets the string for Level Switching;
		
		Time.text = time;
		Deaths.text = deaths;
		Key.value = key;
		Completed.value = completed;
		ScoreLabel.text = Score;
		Debug.Log (Time.text);
	}

	// Update is called once per frame
}
