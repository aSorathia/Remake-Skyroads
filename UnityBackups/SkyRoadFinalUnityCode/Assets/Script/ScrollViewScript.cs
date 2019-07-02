using UnityEngine;
using System.Collections;

public class ScrollViewScript : MonoBehaviour {

	public UISlider slider;
	public float startingValue = 0f;
	public float lastValue = 1f;
	public float division=0f;
	public float currentValue;
	public int StepSize;
	// Use this for initialization
	void Start () {
		StepSize = slider.numberOfSteps;
		currentValue = slider.value;
		division = (1 / (StepSize - 1));
		Debug.Log (StepSize);
		Debug.Log (currentValue);
	}

	public void moveLeft(){
		if ((currentValue <= 1) && (currentValue >= 0)) {
			currentValue = slider.value;
			currentValue = currentValue + division;
			slider.value = currentValue;
			Debug.Log ("left active");
		} else {
			Debug.Log ("I am last");		
		}
	}
	public void moveRight(){
		if ((currentValue <= 1) && (currentValue >= 0)) {
			currentValue = slider.value;
			currentValue=currentValue-division;
			slider.value=currentValue;
		} else {
			Debug.Log ("I am last");
			Debug.Log ("right active");
		}
	}
	void Update()
	{
		currentValue = slider.value;
	}
}
