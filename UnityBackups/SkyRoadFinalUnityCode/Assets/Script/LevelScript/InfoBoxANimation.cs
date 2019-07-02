using UnityEngine;
using System.Collections;

public class InfoBoxANimation : MonoBehaviour {

	public TweenPosition InfoBox;
	public UILabel InfoBoxLabel;
	public string infoTextString="";
	public bool initialFunction=false;
	// Use this for initialization
	void Start () {

	}
	void Update(){
		if(!InfoBoxLabel.GetComponent<TextDisplayInfoBox> ().istypewriter && initialFunction){
		   InfoBoxLabel.GetComponent<TextDisplayInfoBox> ().istypewriter = false;
		   InfoBoxLabel.GetComponent<TextDisplayInfoBox>().DisplayText = infoTextString;//infoTextString;
		   InfoBox.PlayForward ();	
		   initialFunction=false;
		   this.enabled=false;
		}
	}
}