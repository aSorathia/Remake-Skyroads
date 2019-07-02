using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public UILabel TimmerLabel;
	public UILabel BestScoreLabel;

	public Timer timer;


	public float BestTime=99999f;
	bool setOnce=false;
	float startTime=0;


	[Header("Only for Debuging")]
	public float timeTaken=0f;
	public int minutes;
	public int seconds;
	public float fraction;
	public string stringtime;
	public GameObject Teleporter;

	void Start () {	
	//Get the Best Score
		BestTime = PlayerPrefs.GetFloat ("BestTime");
		/*if (BestTime == 0) {
			BestTime=9999f;
		}*/
		BestScoreLabel.text = FormatTime(BestTime);
	}
	

	void Update () {
		//caculate the time difference from start of the game to Overlaystart
		timeTaken=Time.time-startTime;
		TimmerLabel.text = FormatTime (timeTaken);
		if (timeTaken < BestTime) {

			if (!setOnce) {
				PlayerPrefs.SetInt ("newBestScore", 1);
				setOnce = true;
			}
		} else {
			PlayerPrefs.SetInt ("newBestScore", 0);	
		}
	}

	public void GetStartTime(){
		startTime = Time.time;
		//Enable this Script after Overlay animation
		timer.enabled = true;
		Destroy (Teleporter);
	}

	//Format time from seconds to mm:ss:ff
	string FormatTime(float time){
		if(time==999999.0f){
			stringtime = "--:--:--";
		}else{
			float floatTime = time;
			minutes = (int)floatTime / 60;
			seconds  = (int)floatTime % 60;
			fraction  = time * 10;
			fraction = fraction % 10;
			stringtime = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
		}
		return stringtime;
	}
}
