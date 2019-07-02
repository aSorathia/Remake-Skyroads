using UnityEngine;
using System.Collections;

public class LevelRotate : MonoBehaviour {
	float speed=0.5f;
	//public float friction=1;
	//public float lerpSpeed=2;
	float xDeg;
	private float yDeg;
	private Quaternion fromRotation;
	private Quaternion toRotation;

	// Use this for initialization
	void Start () {
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)) 
		{
			xDeg -= Input.GetAxis("Mouse X")+speed;
			yDeg += Input.GetAxis("Mouse Y")+speed ;
			fromRotation = transform.rotation; 
			toRotation = Quaternion.Euler(yDeg,xDeg,0);
			transform.rotation = Quaternion.Euler(yDeg,xDeg,0);//Quaternion.Lerp(fromRotation,toRotation,Time.deltaTime * lerpSpeed); 
		}	
	}
}
