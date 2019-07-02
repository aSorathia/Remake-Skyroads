using UnityEngine;
using System.Collections;

public class StartUpConfigurer : MonoBehaviour {

	public GameObject Level;
	public GameObject DeathField;
	// Use this for initialization
	void Start () {
		object[] obj = GameObject.FindSceneObjectsOfType(typeof (GameObject));
		Level = GameObject.FindGameObjectWithTag ("Ground");
		foreach (object o in obj)
		{
			GameObject g = (GameObject) o;
			if(g.tag=="BK_Burning"||g.tag=="BK_Slippery"||g.tag=="BK_Oxygen"||g.tag=="BK_Sticky"||g.tag=="BK_Boost"){
				Destroy(g.GetComponent<MeshCollider>());
				Destroy(g.GetComponent<BoxCollider>());
				g.AddComponent<BoxCollider>();
				Vector3 Size = g.GetComponent<BoxCollider>().size;
				g.GetComponent<BoxCollider>().size =new Vector3(Size.x,Size.y,0.52f);
				g.GetComponent<BoxCollider>().isTrigger=true;
			}
			if(g.tag=="Goal"){
				Destroy(g.GetComponent<MeshCollider>());
				Destroy(g.GetComponent<BoxCollider>());
				g.AddComponent<BoxCollider>();
				Vector3 Size = g.GetComponent<BoxCollider>().size;
				g.GetComponent<BoxCollider>().size =new Vector3(Size.x,7.83f,0.52f);
				g.GetComponent<BoxCollider>().center =new Vector3(0f,2.4f,0f);
				g.GetComponent<BoxCollider>().isTrigger=true;
			}
		}

		Level.AddComponent<BoxCollider>();
		Vector3 LevelSize = Level.GetComponent<BoxCollider>().size;
		Vector3 LevelCenter = GameObject.FindGameObjectWithTag ("LevelGameObject").transform.position;
		Destroy(Level.GetComponent<BoxCollider>());
		DeathField.transform.position =new Vector3(DeathField.transform.position.x,DeathField.transform.position.y,LevelSize.y / 2f);
		DeathField.AddComponent<BoxCollider>();
		DeathField.GetComponent<BoxCollider>().size = new Vector3(LevelSize.x+216f,LevelSize.y+100,1.2f);
		DeathField.GetComponent<BoxCollider>().center = new Vector3(LevelCenter.x,LevelCenter.y,-7f);
		DeathField.GetComponent<BoxCollider>().isTrigger=true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
