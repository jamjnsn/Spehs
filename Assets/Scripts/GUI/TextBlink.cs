using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class TextBlink : MonoBehaviour {
	public bool On = true;
	public float OnTime = 1f, OffTime = 1f;
	float timer;

	// Use this for initialization
	void Start () {
		timer = On ? OnTime : OffTime;
		guiText.enabled = On;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		
		if(timer <= 0)
		{
			On = !On;
			guiText.enabled = On;
			timer = On ? OnTime : OffTime;
		}
	}
}
