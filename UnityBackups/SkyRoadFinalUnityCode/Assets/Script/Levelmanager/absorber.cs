using UnityEngine;
using System.Collections;

public class absorber : MonoBehaviour {
	public Color normal;
	public Color Selected;
	public TweenWidth tweenWidth;
	public TweenPosition tweenPosition;
	public UIToggle toggle;
	public UILabel label;
	public UILabel StageNumber;
	public UIWidget LevelButtonHolder;
	bool forward=false;
	bool reverse=false;
	bool val;
	bool called=false;
	bool isToggleSet=false;
	bool pressed=false;
	public animLevelButtons[] levelButtons;	
	void Start()
	{
		normal = new Color (1f, 1f, 1f);
		Selected = new Color (1f, 0.847f, 0f);
		val = toggle.value;
		tweenWidth.enabled = val;
		tweenPosition.enabled = val;
		forward = val;
		reverse = val;
		GameObject[] stageButtons=GameObject.FindGameObjectsWithTag("StageAnimButtons");
		int i = 0;
		for(i=0;i<10;i++){
			levelButtons[i]=(animLevelButtons)stageButtons[i].GetComponent("animLevelButtons");
		}
		//Debug.Log ("Total list"+i);
	}
	void Update(){
		val = toggle.value;
		if (val) {
			label.color = Selected;
			StageNumber.text = this.gameObject.name;
		} else {
			label.color = normal;				
		}
	}

	public void LevelButtonVisibility(){
		val = toggle.value;
		NGUITools.SetActive(LevelButtonHolder.gameObject,val);
	}
	/*
	public void hoverIn(){
		if (pressed) {

		} else {
			tweenWidth.PlayForward();
			tweenPosition.PlayForward();
			forward=true;
		}

	}
	public void hoverout(){
		if (forward && !toggle.value) {
			tweenWidth.PlayReverse ();
			tweenPosition.PlayReverse ();
			reverse = true;
			forward = false;
		} else {
			isToggleSet=true;	
			called=true;
		}
	}
*/
	public void click(){
		for(int i=0;i<levelButtons.Length;i++){
			levelButtons[i].tw.PlayReverse();
			levelButtons[i].tp.PlayReverse();
		}
		tweenWidth.PlayForward();
		tweenPosition.PlayForward();

	}

}
