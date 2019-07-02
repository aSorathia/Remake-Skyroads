using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {
	public UISlider Music;
	public UISlider SFX;
	public AudioSource FXThruster;
	public AudioSource MUSIC;
	float musicValue;
	float sfxValue;

	void Start(){
		if(PlayerPrefs.GetInt("firstSet")==0){
			musicValue = PlayerPrefs.GetFloat ("musicVol");
			sfxValue = PlayerPrefs.GetFloat ("sfxVol");
			PlayerPrefs.SetInt("firstSet",1);
		}
		if(PlayerPrefs.GetInt ("issfxVolOn")==1){
			FXThruster.volume = sfxValue;
			SFX.value=sfxValue;
		}else{
			FXThruster.volume = 0f;
			SFX.value = 0f;
		}
		if(PlayerPrefs.GetInt ("isMusicVolOn")==1){
			MUSIC.volume = musicValue;
			Music.value = musicValue;
		}else{
			MUSIC.volume = 0f;
			Music.value = 0f;
		}
		Debug.Log ("sfx val "+sfxValue+" music val "+musicValue+" "+"called start");
	}

	public void updateFX(){
		sfxValue = Mathf.Round(SFX.value*10)/10;
		if(PlayerPrefs.GetInt ("issfxVolOn")==1 && sfxValue>0){
			FXThruster.volume = sfxValue;
			PlayerPrefs.SetFloat ("sfxVol", sfxValue);
			PlayerPrefs.SetInt ("issfxVolOn", 1);

		}else if(PlayerPrefs.GetInt ("issfxVolOn")==0 && sfxValue>0){
			FXThruster.volume = sfxValue;
			PlayerPrefs.SetFloat ("sfxVol", sfxValue);
			PlayerPrefs.SetInt ("issfxVolOn", 1);
			
		}else{
			FXThruster.volume = 0f;
			PlayerPrefs.SetFloat ("sfxVol", 0);
			PlayerPrefs.SetInt ("issfxVolOn", 0);
		}
		Debug.Log ("sfx val "+sfxValue+" music val "+musicValue+" "+"called sfx");
	}

	public void UpdateMusic(){
		musicValue = Mathf.Round( Music.value*10)/10;
		if(PlayerPrefs.GetInt ("isMusicVolOn")==1 && musicValue>0){
			MUSIC.volume = musicValue;		
			PlayerPrefs.SetFloat ("musicVol",musicValue);
			PlayerPrefs.SetInt ("isMusicVolOn", 1);
		}else if(PlayerPrefs.GetInt ("isMusicVolOn")==0 && musicValue>0){
			MUSIC.volume = musicValue;		
			PlayerPrefs.SetFloat ("musicVol",musicValue);
			PlayerPrefs.SetInt ("isMusicVolOn", 1);
		}else{
			MUSIC.volume=0f;
			PlayerPrefs.SetFloat ("musicVol",0f);
			PlayerPrefs.SetInt ("isMusicVolOn", 0);
		}
		Debug.Log ("sfx val "+sfxValue+" music val "+musicValue+" "+"called music");
	}
}
