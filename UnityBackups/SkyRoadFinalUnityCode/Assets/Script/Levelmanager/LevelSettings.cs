using UnityEngine;
using System.Collections;

public class LevelSettings : MonoBehaviour {

	int isMusicVolOn;
	int issfxVolOn;
	float musicVol;
	float sfxVol;
	[Header("Audio Source")]
	public AudioSource[] FX;
	public AudioSource MUSIC;
	[Header("Toggles")]
	public UIToggle sfx;
	public UIToggle Music;
	[Header("GameObjects")]
	public GameObject GameMusic;
	public GameObject LevelManager;
	bool onceS=true;
	bool onceM=true;
	// Use this for initialization
	void Start () {
		isMusicVolOn=PlayerPrefs.GetInt ("isMusicVolOn");
		issfxVolOn=PlayerPrefs.GetInt ("issfxVolOn");
		musicVol=PlayerPrefs.GetFloat ("musicVol");
		sfxVol=PlayerPrefs.GetFloat ("sfxVol");
		Debug.Log (isMusicVolOn +";"+ issfxVolOn +";"+ musicVol +";"+ sfxVol);
		MUSIC.volume = musicVol;
		foreach(AudioSource fx in FX){
			fx.volume=sfxVol;
		}

		if (issfxVolOn==1) {
			sfx.value=false;
			foreach(AudioSource fx in FX){
				fx.enabled=true;
			}
		} else {
			sfx.value=true;
			foreach(AudioSource fx in FX){
				fx.enabled=false;
			}
		}
		if (isMusicVolOn==1) {
			Music.value=false;
			MUSIC.enabled=true;
		} else {
			Music.value=true;
			MUSIC.enabled=false;
		} 
		gameObjectEnabler ();
	}
	void gameObjectEnabler(){
		GameMusic.SetActive (true);
		LevelManager.SetActive (true);
	}
	//isMusicVolOn=PlayerPrefs.GetInt ("isMusicVolOn");
	//issfxVolOn=PlayerPrefs.GetInt ("issfxVolOn");
	void getPresValue(){
		isMusicVolOn=PlayerPrefs.GetInt ("isMusicVolOn");
		issfxVolOn=PlayerPrefs.GetInt ("issfxVolOn");
		musicVol=PlayerPrefs.GetFloat ("musicVol");
		sfxVol=PlayerPrefs.GetFloat ("sfxVol");
	}
	public void sfxEnabler(){
		if(onceS){
			getPresValue();
			if(issfxVolOn==1){
				sfx.value=false;
				foreach(AudioSource fx in FX){
					fx.enabled=true;
				}
			}else{
				issfxVolOn=0;
				foreach(AudioSource fx in FX){
					fx.enabled=false;
				}
			}
			onceS=false;
		}else{
			if(!sfx.value){
				issfxVolOn=1;
				foreach(AudioSource fx in FX){
					fx.enabled=true;
				}
			}else{
				issfxVolOn=0;
				foreach(AudioSource fx in FX){
					fx.enabled=false;
				}
			}
		}
		updateSound ();
	}
	public void musicEnabler(){
		if(onceM){
			getPresValue();
			if (isMusicVolOn==1) {
				isMusicVolOn=1;
				MUSIC.enabled=true;
			} else {
				isMusicVolOn=0;
				MUSIC.enabled=false;
			}
			onceM=false;
		}else{
			if (!Music.value) {
				isMusicVolOn=1;
				MUSIC.enabled=true;
			} else {
				isMusicVolOn=0;
				MUSIC.enabled=false;
			}
		}
		updateSound ();
	}

	void updateSound(){
		PlayerPrefs.SetInt ("issfxVolOn", issfxVolOn);
		PlayerPrefs.SetInt ("isMusicVolOn", isMusicVolOn);
		PlayerPrefs.SetFloat ("musicVol",musicVol);
		PlayerPrefs.SetFloat ("sfxVol",sfxVol);
	}

}
