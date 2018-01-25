using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {
	public AudioClip Sound;
	public string SceneName;
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey || Input.touches.Length > 0)
		{
			GameObject controller = GameObject.FindGameObjectWithTag(Tags.GameController);
			if(controller != null) Destroy(controller.gameObject);
			
			AudioSource.PlayClipAtPoint(Sound, transform.position);
			GetComponent<SceneChanger>().StartSceneChange(SceneName);
		}
	}
}
