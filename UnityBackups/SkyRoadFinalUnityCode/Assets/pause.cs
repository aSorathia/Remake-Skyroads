using System;
using UnityEngine;

public class pause : MonoBehaviour
{
	public GameObject PauseScreen;
	bool paused = false;
	public CameraFollow cf;
	bool goal;
	bool death;
	float musicVol;
	float sfxvol;
	int isMusic;
	int issfx;
	int score;
	public GameObject PauseButton;
	public GameObject QuitDialog;
	void Update()
	{
		death = cf.dead;
		if(Input.GetButtonDown("pauseButton") && !death && !goal){
			if(QuitDialog.activeSelf){
				QuitDialog.SetActive(false);
			}else{
				paused = togglePause();
				PauseScreen.SetActive(paused);
				PauseButton.SetActive(!paused);
				if(paused){
					iTween.Pause();
				}else{
					iTween.Resume();
				}
			}
			print ("pause is called with "+paused+" "+death+" "+goal);
		}
	}
	public void Resume(){
		if(paused)
		{
			paused = togglePause();
			PauseScreen.SetActive(paused);
			PauseButton.SetActive(!paused);
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
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;		
		}
	}

	public void LoadManager()
	{
		Application.LoadLevel("LevelManager");
	}
	public void QuitToMainScreen()
	{
		Application.LoadLevel("MainSCreen");
	}
	public void DisplayQuitOptions(){
		QuitDialog.SetActive (true);
	}
	public void QuitCancel(){
		QuitDialog.SetActive (false);
	}
	public void QuitApp()	{

		Application.Quit();
	}

	public void pauseSystem(){
		death = cf.dead;
		if(!death && !goal){
			paused = togglePause();
			PauseScreen.SetActive(paused);
			PauseButton.SetActive(!paused);
			if(paused){
				iTween.Pause();
			}else{
				iTween.Resume();
			}			
			print ("pause is called with "+paused+" "+death+" "+goal);
		}
	}

	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}