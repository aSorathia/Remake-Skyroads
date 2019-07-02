using UnityEngine;
using System.Collections;

public class TextDisplayInfoBox : MonoBehaviour {
	public string DisplayText;
	public float DelayTime=0f;
	public UILabel InfoLabel;
	public float reverseTweenTime=0f;
	public TweenPosition textInfoBox;
	public bool istypewriter=false;
	public bool isInMotion=false;
	// Use this for initialization

	public void displayTextFunction(){
		if (!istypewriter) {
			DisplayText = DisplayText.Replace("\\n","\n");
			StartCoroutine (Typewritter ());
			istypewriter=true;

		}
		Debug.Log ("Type writter called");
	}
	// Update is called once per frame
	IEnumerator Typewritter(){
		foreach (char letter in DisplayText.ToCharArray()) {
			InfoLabel.text += letter;				
			yield return new WaitForSeconds (DelayTime);
		}
		yield return new WaitForSeconds (reverseTweenTime);
		textInfoBox.PlayReverse ();
		EventDelegate.Set(textInfoBox.onFinished, enableTypeWritter); 
		InfoLabel.text = "";
		Debug.Log ("typing text done");
	}
	public void enableTypeWritter(){
		istypewriter = false;
	}
}
