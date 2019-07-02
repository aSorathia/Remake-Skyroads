using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;

public class LevelManager : MonoBehaviour {
	protected SqliteConnection dbcon;
	public DataStructure[] NoOfLevels;
	public StageSetter[] Stage;
	int arrayLength;
	public GameObject UIRoot;


	int isMusicVolOn;
	int issfxVolOn;
	float musicVol;
	float sfxVol;

	void Start(){
		Debug.Log ("called 2");
		Debug.Log("level manager");
		isMusicVolOn=PlayerPrefs.GetInt ("isMusicVolOn");
		issfxVolOn=PlayerPrefs.GetInt ("issfxVolOn");
		musicVol=PlayerPrefs.GetFloat ("musicVol");
		sfxVol=PlayerPrefs.GetFloat ("sfxVol");
		arrayLength = NoOfLevels.Length;
		Debug.Log ("Level = "+PlayerPrefs.GetString ("levelButtonName")+" and length = "+arrayLength);
		String DefaultLevel="Level11";

		if((PlayerPrefs.GetInt("isPlayerPrefSet")==1)&&(PlayerPrefs.GetInt("newBestScore")==1))
		{
			Debug.Log ("Playerpref set Goal");
			int completed=PlayerPrefs.GetInt("Completed");
			float time= PlayerPrefs.GetFloat ("Time");
			int FoundKey=PlayerPrefs.GetInt("FoundKey");
			int star=PlayerPrefs.GetInt("stars");
			int level=PlayerPrefs.GetInt("Level");
			int stage=PlayerPrefs.GetInt("Stage");
			int deaths=PlayerPrefs.GetInt("Death");
			int Score=PlayerPrefs.GetInt("Score");
			 /* set playerprefs in database
		 	 * load database;
			 */
			Debug.Log(PlayerPrefs.GetFloat ("Time")+"total time from Levelmanager");
			String updateString;
			updateString="UPDATE LevelManager SET Completed="+completed+",Time="+time+",Stars="+star+",Key="+FoundKey+",Score="+Score+" where Level='Level"+stage+""+level+"'";
			

			level++;
			if(level==4)
			{
				stage++;
				level=1;
			}
			String updateString2="UPDATE LevelManager SET Unlocked="+1+" where Level='Level"+stage+""+level+"'";
			loadDataBase();
			try
			{
				dbcon.Open ();
				UpdateDB(updateString);
				UpdateDB(updateString2);
				DefaultLevel=PlayerPrefs.GetString ("levelButtonName");
				PlayerPrefs.DeleteAll();
				PlayerPrefs.SetString ("levelButtonName", DefaultLevel);
				getDbValues();
			}
			catch(Exception e){
				Debug.Log("Playerpref set "+e);
			}
			closeDataBase ();

		}
		else if(PlayerPrefs.GetInt("isPlayerPrefSet")==2){

			Debug.Log ("Playerpref set Death");
			int FoundKey=PlayerPrefs.GetInt("FoundKey");
			int level=PlayerPrefs.GetInt("Level");
			int stage=PlayerPrefs.GetInt("Stage");
			int deaths=PlayerPrefs.GetInt("Death");
			
			/* set playerprefs in database
		 	 * load database;
			 */
			Debug.Log ("Playerpref set");
			String updateString="UPDATE LevelManager SET Key="+FoundKey+",Deaths="+deaths+" where Level='Level"+stage+""+level+"'";
			Debug.Log (updateString);
			loadDataBase();
			try
			{
				dbcon.Open ();
				UpdateDB(updateString);
				DefaultLevel=PlayerPrefs.GetString ("levelButtonName");
				PlayerPrefs.DeleteAll();
				PlayerPrefs.SetString ("levelButtonName", DefaultLevel);
				getDbValues();
			}
			catch(Exception e){
				Debug.Log("Playerpref set "+e);
			}
			closeDataBase ();
		}
		else
		{	Debug.Log ("Playerpref Not set Normal Load");
			loadDataBase();
			try
			{
				dbcon.Open ();
				PlayerPrefs.DeleteAll();
				getDbValues();
			}
			catch(Exception e){
				Debug.Log("Playerpref Not set "+e);
			}
			closeDataBase ();
		}
		UIRoot.SetActive (true);
		updateSound ();
	}
	void updateSound(){
		Debug.Log ("Update sound called LM");
		PlayerPrefs.SetInt ("issfxVolOn", issfxVolOn);
		PlayerPrefs.SetInt ("isMusicVolOn", isMusicVolOn);
		PlayerPrefs.SetFloat ("musicVol",musicVol);
		PlayerPrefs.SetFloat ("sfxVol",sfxVol);
	}
	void loadDataBase()
	{
		string connectionString = "URI=file:" + Application.persistentDataPath + "/GameMaster/GameMaster"; //Path to database.;
		Debug.Log (connectionString);		
		dbcon = new SqliteConnection (connectionString);
		Debug.Log ("Open Database");
	}
	void UpdateDB(String insertQuery){
		Debug.Log ("Update Database");
		try {
			SqliteCommand cmd = new SqliteCommand (insertQuery, dbcon);
			int i=cmd.ExecuteNonQuery();
			if(i==1)
			{
				Debug.Log ("Sccess");
			}else
			{
				Debug.Log ("Fail");
			}
		} catch(Exception e) {
			Debug.Log ("Error Updateing DB "+e);
		}
	}
	void getDbValues()
	{
		Debug.Log ("Get Database");
		int val = 0;	
		try {
			String sql = "SELECT Level, Completed, Time, Stars, Key, Unlocked, Deaths, Score FROM Levelmanager";
			SqliteCommand cmd = new SqliteCommand (sql, dbcon);
			SqliteDataReader reader = cmd.ExecuteReader ();
			if (reader.HasRows) {
				while (reader.Read() && val < arrayLength) {
					string Level = reader.GetString (0);
					bool Completed = reader.GetBoolean (1);
					float floatTime=reader.GetFloat (2);
					String time = FormatTime(floatTime );
					int rating=reader.GetInt16 (3);
					bool Key = reader.GetBoolean (4);
					bool locked=reader.GetBoolean(5);
					int deaths=reader.GetInt16(6);
					int score=reader.GetInt32(7);
					Debug.Log (Level+", "+val+", "+Completed+", "+time+", "+rating+", "+Key+", "+locked+", "+deaths);
					setValues(Level,val,Completed,time,floatTime,rating,Key,locked,deaths,score);
					val++;
				}
			}else{
				Debug.Log ("No rows left");
			}
			
			reader.Close ();
			reader = null;
		} catch(Exception e) {
			Debug.Log ("Error Getting Values from DB "+e);
		}
	}
	void setValues(String Level,int val,bool Completed,String time,float floatTime,int rating,bool Key,bool locked,int death,int score)
	{
		try{
			int num=val/3;
			if(val==0||val==3||val==6||val==9||val==12||val==15||val==18||val==21||val==24||val==27){
				Stage [num].ratingValue = rating;
				Stage [num].time = time;
				Stage [num].deaths = death.ToString();
				Stage [num].completed = Completed;
				Stage [num].key = Key;
				Stage [num].Score = score.ToString();
			}
		}catch(Exception e){
			Debug.Log ("Error Seetting Stage Button Vaules from DB "+e);
		}

		try{
			NoOfLevels[val].Button.isEnabled=locked;
			NoOfLevels[val].LockSprite.enabled=!locked;
			NoOfLevels [val].levelData.ratingValue = rating;
			NoOfLevels [val].levelData.time = time;
			NoOfLevels [val].levelData.floatTime = floatTime;
			NoOfLevels [val].levelData.intDeaths = death;
			NoOfLevels [val].levelData.deaths = death.ToString();
			NoOfLevels [val].levelData.completed = Completed;
			NoOfLevels [val].levelData.key = Key;
			NoOfLevels [val].levelData.Score = score.ToString();
			NoOfLevels [val].led.value=Completed;

		}catch(Exception e){
			Debug.Log ("Error Seetting LevelButton Vaules from DB "+e);
		}
	}

	void closeDataBase()
	{
		Debug.Log ("Close Database");
		dbcon.Close ();
		dbcon = null;
	}

	string FormatTime(float time){
		string stringtime;
		if(time==999999){
			stringtime="00:00:00";
		}
		else{
			float floatTime = time;
			int minutes = (int)floatTime / 60;
			int seconds  = (int)floatTime % 60;
			float fraction  = time * 10;
			fraction = fraction % 10;
			stringtime = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
		}
		return stringtime;
	}
}
