using UnityEngine;
using System.Collections;
using System.IO;
public class ValidationScript : MonoBehaviour {

	public ConfigFile cf;

	public GameObject GameTitle;
	public GameObject MenuSelection;
	public GameObject InfoPanel;
	public GameObject registrationForm;

	public UIInput nameInput;
	public UIToggle male;
	public UIToggle female;
	public UIInput day;
	public UIInput month;
	public UIInput year;
	public UILabel dname;
	public UILabel dgender;
	public UILabel dday;
	public UILabel dmonth;
	public UILabel dyear;
	bool validInput=true;
	int[] monthLen=new int[]{31,28,31,30,31,30,31,31,30,31,30,31};
	public GameObject OkNormal;
	public GameObject OkPressed;
	public GameObject rNormal;
	public GameObject rPressed;
	/*
		jan
		feb 28
		mar
		apr 30
		may
		jun 30
		july
		aug
		sep 30
		oct
		nov 30
		dec
	 */
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ButtonRelease(){
		OkNormal.SetActive (true);
		OkPressed.SetActive (false);
	}
	public void ButtonPress(){
		OkNormal.SetActive  (false);
		OkPressed.SetActive  (true);
	}
	public void rButtonRelease(){
		rNormal.SetActive (true);
		rPressed.SetActive (false);
	}
	public void rButtonPress(){
		rNormal.SetActive  (false);
		rPressed.SetActive  (true);
	}
	public void validateInputs(){
		if (nameInput.value == "" || nameInput.value == " ") {
			validInput=false;
			dname.color=new Color(255,190,0);
		}else{
			dname.color=Color.black;
			validInput=true;
		}
		if(male.value==false && female.value==false){
			validInput=false;
			dgender.color=new Color(255,190,0);
		}else{
			dgender.color=Color.black;
			validInput=true;
		}

		if(year.value==""|| year.value==" "){
			validInput=false;
			dyear.color=new Color(255,190,0);
		}else if(int.Parse(year.value)<1915 || int.Parse(year.value)>2015){
			validInput=false;
			dyear.color=new Color(255,190,0);
		}else{
			dyear.color=Color.black;
			validInput=true;
		}

		if(month.value==""|| month.value==" "){
			validInput=false;
			dmonth.color=new Color(255,190,0);
		}else if(int.Parse(month.value)<1 || int.Parse(month.value)>12){
			validInput=false;
			dmonth.color=new Color(255,190,0);
		}else{
			dmonth.color=Color.black;
			validInput=true;
			int monthNum=int.Parse(month.value);
			if(day.value==""|| day.value==" "){
				validInput=false;
				dday.color=new Color(255,190,0);
			}else if(int.Parse(day.value)<1 ||int.Parse(day.value)>monthLen[--monthNum]){
				validInput=false;
				dday.color=new Color(255,190,0);
			}else{
				dday.color=Color.black;
				validInput=true;
			}
		}
		if(day.value==""|| day.value==" "){
			validInput=false;
			dday.color=new Color(255,190,0);
		}

		print("valid iput"+validInput);
		if(validInput){
			validationTrue();
		}
		Debug.Log ("Width= "+Screen.width+"height= "+Screen.height);
		Debug.Log ("Resolution= "+Screen.GetResolution+"dpi= "+Screen.dpi);

	}
	public void resetInputs(){
		dname.color=Color.black;
		dgender.color=Color.black;
		dyear.color=Color.black;
		dmonth.color=Color.black;
		dday.color=Color.black;
		nameInput.value = "";
		month.value = "";
		day.value = "";
		year.value = "";
	}
	public void validationTrue(){
		dname.color=Color.black;
		dgender.color=Color.black;
		dyear.color=Color.black;
		dmonth.color=Color.black;
		dday.color=Color.black;
		GameTitle.SetActive (true);
		MenuSelection.SetActive (true);
		InfoPanel.SetActive (true);
		registrationForm.SetActive (false);
		File.Create (Application.persistentDataPath + "/" + "registrationDone");
	}
}
