using UnityEngine;
using System.Collections;

public class LoadLevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void LoadManager()
	{
		Application.LoadLevel("LevelManager");
	}
	public void LoadMainMenu()
	{
		Application.LoadLevel("MainSCreen");
	}
}
