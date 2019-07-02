using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour {

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
	public float floatTime=0f;
	public int intDeaths=0;
	public UILabel ScoreLabel;
	public string Score="";
	public string gamePlayName="";
	public MMT.MobileMovieTexture mMT;
	public void setLevelData()
	{

		/*if (positionPrefab.childCount > 0) {
			foreach(Transform child in positionPrefab) {
				Destroy(child.gameObject);
			}	
		}
		GameObject gb = (GameObject)Instantiate (LevelPrefab);
		gb.transform.parent = positionPrefab;*/
		//To initilize Rating Stats
		for (int i=1; i<=5; i++) {
			ratingSprite[i].enabled=false;	
		}
		if (ratingValue != 0) {
			ratingSprite [ratingValue].enabled = true;		
		} else {
			Debug.Log ("Am Zero");
		}
		//Sets the string for Level Switching;
		PlayerPrefs.SetString ("levelButtonName", Levelname);

		Time.text = time;
		Deaths.text = deaths;
		Key.value = key;
		Completed.value = completed;
		ScoreLabel.text = Score;
		PlayerPrefs.SetFloat ("BestTime",floatTime);
		PlayerPrefs.SetInt ("Death",intDeaths);

	}
	public void LoadLevel()
	{
		Debug.Log (PlayerPrefs.GetString ("levelButtonName"));
		Application.LoadLevel(PlayerPrefs.GetString("levelButtonName"));
	}
	public void playMovie(){
		if(mMT.IsPlaying){
			mMT.Stop();
		}
		string videoPath = "GamePlay/"+gamePlayName+".ogv";
		mMT.Path = videoPath;
		Debug.Log (mMT.Path);
		mMT.Play ();
	}
}
