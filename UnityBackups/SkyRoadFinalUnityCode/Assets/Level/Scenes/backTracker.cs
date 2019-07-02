using UnityEngine;
using System.Collections;
using System.IO;

public class backTracker : MonoBehaviour {

	public GameObject RatingDialog;

	// Use this for initialization
	void Start () {
		RatingDialog.SetActive (false);
	}

	public void displayRating(){
				if(Time.timeScale == 0f)
				{
					Time.timeScale = 1f;
				}
				else
				{
					Time.timeScale = 0f;		
				}
				RatingDialog.SetActive(true);	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			if(!File.Exists(Application.persistentDataPath + "/" + "ratingDone")&&!File.Exists(Application.persistentDataPath + "/" + "ratingNo")){
				if(Time.timeScale == 0f)
				{
					Time.timeScale = 1f;
				}
				else
				{
					Time.timeScale = 0f;		
				}
				RatingDialog.SetActive(true);
			}
		}

	}
}
