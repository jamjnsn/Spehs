using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	bool changingScene = false;
	string targetScene = "";
	SceneFader sceneFader;

	void Awake()
	{
		sceneFader = GameObject.Find("Scene Fader").GetComponent<SceneFader>();
	}

	void Update()
	{
		if(changingScene)
			if(sceneFader.FadedOut)
				ChangeScene();
	}

	public void StartSceneChange(string sceneName)
	{
		targetScene = sceneName;
		changingScene = true;
		sceneFader.FadeOut();
	}
	
	void ChangeScene()
	{
		Application.LoadLevel(targetScene);
		targetScene = "";
		changingScene = false;
		sceneFader.FadeIn();
	}
}
