using UnityEngine;
using System.Collections;

public class DownloadObbExample : MonoBehaviour {

	private string expPath;
	private string logtxt;
	private bool alreadyLogged = false;
	private string nextScene = "Intro";
	private bool downloadStarted;
	public Texture2D background;
	public GUISkin mySkin;

	void OnGUI()
	{
		GUI.skin = mySkin;
		GUI.DrawTexture(new Rect(0,0,background.width,background.height),background);

		if (!GooglePlayDownloader.RunningOnAndroid())
		{
			GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
			return;
		}
		
		expPath = GooglePlayDownloader.GetExpansionFilePath();
		if (expPath == null)
		{
				GUI.Label(new Rect(10, 10, Screen.width-10, 20), "External storage is not available!");
		}
		else
		{
			string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
			string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);

			if( alreadyLogged == false )
			{
				alreadyLogged = true;
				Debug.Log( "expPath = "  + expPath );
				Debug.Log( "Main = "  + mainPath );
				Debug.Log( "Main = " + mainPath.Substring(expPath.Length));
				
				if (mainPath != null)
					StartCoroutine(loadLevel());
				
			}
			if (mainPath == null){
				GUI.Label(new Rect(Screen.width-600, Screen.height-230, 430, 60), "There seems ato be Problem while Downloading Files Click on Fetch File to do it Mannually");
				if (GUI.Button(new Rect(10, 100, 100, 100), "Fetch Files")){
					GooglePlayDownloader.FetchOBB();
					StartCoroutine(loadLevel());
				}
			}
		}

	}
	protected IEnumerator loadLevel() 
	{ 
		string mainPath;
		do
		{
			yield return new WaitForSeconds(0.5f);
			mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);	
			Debug.Log("waiting mainPath "+mainPath);
		}
		while( mainPath == null);
		
		if( downloadStarted == false )
		{
			downloadStarted = true;
			
			string uri = "file://" + mainPath;
			Debug.Log("downloading " + uri);
			WWW www = WWW.LoadFromCacheOrDownload(uri , 0);		
			
			// Wait for download to complete
			yield return www;
			
			if (www.error != null){
				Debug.Log ("wwww error " + www.error);
			}
			else{
				Application.LoadLevel(nextScene);
			}
		}
	}
}
