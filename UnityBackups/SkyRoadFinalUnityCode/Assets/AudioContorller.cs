using UnityEngine;
using System.Collections;

public class AudioContorller : MonoBehaviour {

	public AudioClip[] FX;
	public AudioClip MUSIC;
	int isFx;
	int isMusic;
	// Use this for initialization
	void Start () {
		//PlayerPrefs.GetInt ("FX");
		//PlayerPrefs.GetInt ("Music");
		initializeAudioSettings (isFx,isMusic);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void initializeAudioSettings(int fx,int music){
		if(fx==1){

		}
	}
}
