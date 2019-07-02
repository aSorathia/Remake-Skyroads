using UnityEngine;
using System.Collections;

public class IntroPlayer : MonoBehaviour {

	private string movPath = "IntroComp.mp4";
	public string levelToLoad = "";

	// Use this for initialization
	void Start () {
		Handheld.PlayFullScreenMovie(movPath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
		if (Application.CanStreamedLevelBeLoaded (levelToLoad))
		{
			Debug.Log ("all ok");
			Application.LoadLevel (levelToLoad);
		}
	}
	private IEnumerator PlayStreamingVideo(string url)
	{

		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Debug.Log("Video playback completed.");
	}
}