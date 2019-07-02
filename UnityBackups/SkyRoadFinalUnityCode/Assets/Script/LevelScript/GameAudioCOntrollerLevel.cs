using UnityEngine;
using System.Collections;

public class GameAudioCOntrollerLevel : MonoBehaviour {
	int isMusicVolOn=0;
	int issfxVolOn=0;
	float musicVol=0f;
	float sfxVol=0f;
	[Header("Audio Source")]
	public AudioSource[] FXSource;
	public AudioSource MUSICSource;
	public AudioClip[] FXClips;
	[Header("Toggles")]
	public UIToggle sfx;
	public UIToggle Music;
	bool isvalueInitialized=false;
	[Header("GameObjects")]
	public GameObject BackgroundCamera;
	//public GameObject Level;
	public GameObject DeathTrap;
	public GameObject ShipInstance;
	public GameObject GameCamera;
	public GameObject Key;
	public GameObject Teleporter;
	public GameObject PauseButton;

	// Use this for initialization
	void Start () {
		//Level = GameObject.FindGameObjectWithTag ("LevelGameObject");
		isMusicVolOn=PlayerPrefs.GetInt ("isMusicVolOn");
		issfxVolOn=PlayerPrefs.GetInt ("issfxVolOn");
		musicVol=PlayerPrefs.GetFloat ("musicVol");
		sfxVol=PlayerPrefs.GetFloat ("sfxVol");
		Debug.Log ("ISMUSIC "+isMusicVolOn +";"+"ISSFX "+issfxVolOn +";"+"musvol "+ musicVol +";"+"sfxvol "+ sfxVol);
		MUSICSource.volume = musicVol;
		foreach(AudioSource fx in FXSource){
			fx.volume=sfxVol;
		}
		
		if (issfxVolOn==1) {
			sfx.value=false;
			foreach(AudioSource fx in FXSource){
				fx.enabled=true;
			}
		} else {
			sfx.value=true;
			foreach(AudioSource fx in FXSource){
				fx.enabled=false;
			}
		}
		if (isMusicVolOn==1) {
			Music.value=false;
			MUSICSource.enabled=true;
		} else {
			Music.value=true;
			MUSICSource.enabled=false;
		}
		isvalueInitialized = true;

//		BackgroundCamera.SetActive (true);
		//Level.SetActive (true);
		DeathTrap.SetActive (true);
		ShipInstance.SetActive (true);
		GameCamera.SetActive (true);
		if(Key!=null){
			Key.SetActive(true);
		}
		Teleporter.SetActive (true);
		PauseButton.SetActive (true);
	}
	
	public void sfxEnabler(){
		Debug.Log ("sfx called");
		if(isvalueInitialized){
			if(!sfx.value){
				issfxVolOn=1;
				foreach(AudioSource fx in FXSource){
					fx.enabled=true;
				}
			}else{
				issfxVolOn=0;
				foreach(AudioSource fx in FXSource){
					fx.enabled=false;
				}
			}
		}

	}
	public void musicEnabler(){
		Debug.Log ("Music called");
		if (isvalueInitialized) {
			if (!Music.value) {
				isMusicVolOn=1;
				MUSICSource.enabled=true;
			} else {
				isMusicVolOn=0;
				MUSICSource.enabled=false;
			}
		}
	}
	
	void Update(){
		PlayerPrefs.SetInt ("issfxVolOn", issfxVolOn);
		PlayerPrefs.SetInt ("isMusicVolOn", isMusicVolOn);
		PlayerPrefs.SetFloat ("musicVol",musicVol);
		PlayerPrefs.SetFloat ("sfxVol",sfxVol);
	}
}
