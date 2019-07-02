using UnityEngine;
using System.Collections;

public class AnimationsController : MonoBehaviour {
	public Animation[] ratingAnimation;
	public Animation NoStarAnimation;
	public Animation thumbsupAnimation;
	public Animation test;
	public UILabel TimeLabel;
	public UILabel ScoreLabel;
	public UILabel FoundKey;
	public bool play=false;
	public bool played=false;
	public bool reset=false;
	float timetaken;
	bool startScoreAnimation=false;
	bool startTimerAnimation=false;
	int rating;
	int minutes;
	int seconds;
	string TimeString;
	float fractions;
	int min=0;
	int sec=0;
	float frac=0;
	int Score=0;
	int ScoreCount=0;
	int isBestScore = 0;
	bool foundKey=false;
	float musicVol;
	float sfxvol;
	int isMusic;
	int issfx;
	int score;
	public GoogleAdsScript gAS;
	bool done=false;
	// Use this for initialization
	void Start () {
		gAS = Camera.main.GetComponent<GoogleAdsScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (play && !played) {
			test.Play();
			played=true;
		}
		if (reset) {
			play=false;
			played=false;
			reset=false;
		}

		if(startTimerAnimation){
			if(sec!=seconds){
				sec++;			
			}
			if(min!=minutes){
				min++;
			}
			if(frac!=fractions){
				frac=frac+0.1f;
				frac=Mathf.Round(frac*10f)/10f;
			}
			if(min==minutes && frac==fractions &&sec==seconds){
				startTimerAnimation=false;
				startScoreAnimation=true;
			}
			TimeLabel.text=string.Format ("{0:00}:{1:00}:{2:00}", min, sec, frac);
		}
		if(startScoreAnimation){/*124980*/
			int temp = Score-ScoreCount;
			if(temp==1 || temp==2 || temp==3 || temp==4 || temp==5 || temp==6 || temp==7 || temp==8 || temp==9 || temp==0){
				ScoreCount=ScoreCount+temp;
			}else if(temp>=10 && temp<99){
				ScoreCount++;			
			}else if(temp>=100 && temp<999){
				ScoreCount=ScoreCount+10;
			}else if(temp>=1000 && temp<9999){
				ScoreCount=ScoreCount+100;			
			}else if(temp>=10000){
				ScoreCount=ScoreCount+1000;			
			}else{
				ScoreCount=Score;
				Debug.Log ("cannot move further");
			}
			ScoreLabel.text=ScoreCount+"";
			if(ScoreCount==Score || ScoreCount>999999 || ScoreCount<0){
				startScoreAnimation=false;
				foundKey=true;
			}
		}
		if(foundKey){
			int key=PlayerPrefs.GetInt("FoundKey");
			FoundKey.text=key+"/1";
			isBestScore=PlayerPrefs.GetInt ("newBestScore");
			Debug.Log("bestTime= "+isBestScore);
			foundKey=false;
			if(isBestScore==0){
				gAS.showFullScreen ();
			}
		}
		if (isBestScore==1) {
			thumbsupAnimation.Play();
			isBestScore=0;
			Debug.Log ("Best is played");
			gAS.showFullScreen ();
		}	
	
	}
	public void Reload(){
		musicVol=PlayerPrefs.GetFloat("musicVol");
		sfxvol=PlayerPrefs.GetFloat("sfxvol");
		isMusic=PlayerPrefs.GetInt("isMusicVolOn");
		issfx=PlayerPrefs.GetInt("issfxVolOn");
		score=PlayerPrefs.GetInt("newBestScore");
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("issfxVolOn", issfx);
		PlayerPrefs.SetInt ("isMusicVolOn", isMusic);
		PlayerPrefs.SetFloat ("musicVol",musicVol);
		PlayerPrefs.SetFloat ("sfxVol",sfxvol);
		Application.LoadLevel(Application.loadedLevelName);
	}
	public void initializeAnimation(){
		int count = 0;
		int played = 0;
		rating = PlayerPrefs.GetInt ("stars");
		//rating = 0;
		TimeString = PlayerPrefs.GetString ("Time");
		StartCoroutine(QueueAnim());
		Debug.Log ("start to finish completed");
	}
	
	IEnumerator QueueAnim(){
		int index = 0;
		while(index<rating){
			ratingAnimation[index].Play();
			yield return new WaitForSeconds(ratingAnimation[index].clip.length);
			index++;
		}
		if (rating == 0) {
			NoStarAnimation.Play();
			yield return new WaitForSeconds(NoStarAnimation.clip.length);
		}

		minutes = PlayerPrefs.GetInt ("timeMinutes"); 
		seconds = PlayerPrefs.GetInt ("timeSeconds");
		fractions = PlayerPrefs.GetFloat ("timeFractions");
		fractions=Mathf.Round(fractions*10f)/10f;
		timetaken = PlayerPrefs.GetFloat ("timeTaken");
		if (rating == 0) {
			Score=0;
		} else {
			calculateScore ();
		}
		startTimerAnimation = true;
	}

	void calculateScore(){
		int scoreLimit = 9999999;
		int multiplierSet = 100;
		int multiplier = 10;
		int roundOffTime = (int)Mathf.Round (timetaken * multiplierSet);
		int scoreSet = scoreLimit - roundOffTime;
		if (scoreSet < 0) {
			scoreSet=0;		
		}
		if (scoreSet > scoreLimit) {
			scoreSet=scoreLimit;		
		}
		Score = (int)Mathf.Round(scoreSet/80);
		PlayerPrefs.SetInt ("Score", Score);
	}
}