using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class GUITextShadow : MonoBehaviour {
	public Color Color = Color.black;
	public Vector2 Offset = Vector2.zero;
	private GameObject textShadow;

	// Use this for initialization
	void Start () {
		textShadow = new GameObject(this.name + " (Shadow)");
		textShadow.transform.parent = this.transform;
		textShadow.AddComponent<GUIText>();
		CopySettings();
	}
	
	void FixedUpdate()
	{
		CopySettings();
	}
	
	void CopySettings()
	{
		textShadow.guiText.font = guiText.font;
		textShadow.guiText.fontSize = guiText.fontSize;
		textShadow.guiText.color = Color;
		textShadow.guiText.text = guiText.text;
		textShadow.guiText.anchor = guiText.anchor;
		textShadow.guiText.alignment = guiText.alignment;
		textShadow.transform.localPosition = new Vector3(Offset.x, Offset.y, this.transform.position.z - 1);
	}
}
