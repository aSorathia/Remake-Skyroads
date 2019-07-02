using UnityEngine;
using System.Collections;

public class TouchControllerScript : MonoBehaviour {

	public UISlider vertiSlider;

	public void retractToOriginal(){
		vertiSlider.value = 0.5f;
	}
}
