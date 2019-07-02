using UnityEngine;
using System.Collections;
using System.IO;
public class RatingScript : MonoBehaviour {

	public GameObject ratingDialog;

	// Use this for initialization
	public void rateApp(){
		Application.OpenURL("market://details?id=com.asapps.remakeroads/");
		if(!File.Exists(Application.persistentDataPath + "/" + "ratingDone")){
			if(!File.Exists(Application.persistentDataPath + "/" + "ratingNo")){
				File.Create (Application.persistentDataPath + "/" + "ratingDone");
			}
		}
		ratingDialog.SetActive (false);
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;		
		}
	}
	public void noThanks(){
		if(!File.Exists(Application.persistentDataPath + "/" + "ratingNo")){
			if(!File.Exists(Application.persistentDataPath + "/" + "ratingDone")){
				File.Create (Application.persistentDataPath + "/" + "ratingNo");
			}
		}
		ratingDialog.SetActive (false);
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		else
		{
			Time.timeScale = 0f;		
		}
	}
}
