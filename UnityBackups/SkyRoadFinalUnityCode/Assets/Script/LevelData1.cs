using UnityEngine;
using System.Collections;

public class LevelData1 : MonoBehaviour {

	public string time=" ";
	public bool levelComplete=false;
	public int foundKey=0;
	public int star=0;
	public int stage;
	public int level;
	public int death=0;
	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();
	}

	/*Even if death or goal completed call this function*/
	public void InitializePlayerPref(){

		if (levelComplete) {
			PlayerPrefs.SetInt ("isPlayerPrefSet", 1);
			PlayerPrefs.SetInt("Stage",stage);
			PlayerPrefs.SetInt("Level",level);
			//PlayerPrefs.SetInt("Completed",1);
			PlayerPrefs.SetString("Time", time);
			PlayerPrefs.SetInt("FoundKey",foundKey);
			PlayerPrefs.SetInt("stars",star);
			//PlayerPrefs.SetInt("Death",death);
		}

		Application.LoadLevel ("LevelManager");
	}
}
