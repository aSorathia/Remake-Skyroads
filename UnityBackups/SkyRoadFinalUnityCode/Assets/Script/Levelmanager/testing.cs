using UnityEngine;
using System.Collections;

public class testing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("is pro " + Application.HasProLicense());

		Debug.Log("Application.dataPath "+Application.dataPath );
		Debug.Log("is app alterted after build "+Application.genuine);
		Debug.Log("is application integrity can be confirmed. "+Application.genuineCheckAvailable);

		Debug.Log("Is the current Runtime platform a known console platform. "+Application.platform);
		Debug.Log("Contains the path to a persistent data directory (Read Only). "+Application.persistentDataPath);
		Debug.Log("the platform the game is running  "+Application.platform);
	}
	public void backButton(){
		Application.LoadLevel ("MainSCreen");	
	}
	public void captureScreenShot(){
		if (System.IO.File.Exists (Application.persistentDataPath + "/SkyRoadsRemake.png")) {
						Debug.Log ("Exists");
				} else {
						Debug.Log ("No");
				}
		//string fileName;
		//Application.CaptureScreenshot (Application.persistentDataPath+"/SkyRoadsRemake");
	}
}
