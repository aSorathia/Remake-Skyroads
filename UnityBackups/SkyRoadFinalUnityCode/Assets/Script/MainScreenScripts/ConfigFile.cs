using UnityEngine;
using System.Collections;
using System.IO;
public class ConfigFile : MonoBehaviour {

	public GameObject GameTitle;
	public GameObject MenuSelection;
	public GameObject InfoPanel;
	public GameObject uiRoot;
	public GameObject registrationForm;
	int dRegistration=0;
	string databaseName="GameMaster";
	string dPath="GameMaster";
	string s_AssetsPath="";
	WWW www;
	void copyDB(){
		#if UNITY_EDITOR
		File.Copy(s_AssetsPath+"GameMaster/GameMaster",Application.persistentDataPath+"/"+dPath+"/"+databaseName);
		#elif UNITY_IPHONE
		
		#elif UNITY_ANDROID
		WWW loadDB = new WWW(s_AssetsPath+"GameMaster/GameMaster");
		Debug.Log ("db while started");	
		while(!loadDB.isDone) {}  	
		Debug.Log ("db while finished and copy started");	
		File.WriteAllBytes(Application.persistentDataPath+"/"+dPath+"/"+databaseName, loadDB.bytes);
		Debug.Log ("db copy finished");	
		#endif
		
	}
	void Start () {
		#if UNITY_EDITOR
		s_AssetsPath=Application.dataPath + "/StreamingAssets/";	
		Debug.Log ("Config file unity editor");		
		#elif UNITY_IPHONE
		s_AssetsPath=Application.dataPath + "/Raw/";
		Debug.Log ("Config file UNITY_IPHONE editor");
		pathWWW();
		#elif UNITY_ANDROID
		s_AssetsPath = "jar:file://" + Application.dataPath + "!/assets/";
		Debug.Log ("Config file UNITY_ANDROID editor");	
		#endif
		
		if(Directory.Exists(Application.persistentDataPath + "/" + dPath)){
			bool allOK=true;
			if(!File.Exists(Application.persistentDataPath + "/" + dPath+"/"+databaseName)){
				copyDB();
			}
		}else{
			Directory.CreateDirectory(Application.persistentDataPath + "/" + dPath);
			copyDB();
		}
		if(!File.Exists(Application.persistentDataPath + "/" + "registrationDone")){
			registrationForm.SetActive(true);
		}else{
			GameTitle.SetActive (true);
			MenuSelection.SetActive (true);
			InfoPanel.SetActive (true);
			registrationForm.SetActive (false);
		}
		uiRoot.SetActive (true);
	}

	void Update () {
		
	}
}
