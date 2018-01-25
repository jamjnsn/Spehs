using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject Object;
	public float Frequency;
	public int TeamID;
	
	float timer;
	bool invalid = false;
	
	void Start()
	{
		timer = Frequency;
		
		if(Object == null || Object.GetComponent<Ship>() == null)
		{
			Debug.Log ("Invalid spawner object: no Ship component found.");
			invalid = true;
			name += " (Invalid!)";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(invalid == true) return;
	
		timer -= Time.deltaTime;
		
		if(timer <= 0f)
		{
			GameObject obj = Instantiate (Object) as GameObject;
			obj.transform.parent = GameObject.FindWithTag(Tags.GameController).transform;
			obj.transform.position = this.transform.position;
			obj.GetComponent<Ship>().SetTeam(TeamID);
			timer = Frequency;
		}
	}
}
