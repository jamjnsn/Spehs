using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedGradient : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	int currentIndex;
	
	public float Speed;
	public Color[] Colors;
	public float ChangeThreshold;

	// Use this for initialization
	void Start () {
		currentIndex = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Colors.Length == 0) return;
	
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, Colors[currentIndex], Speed * Time.deltaTime);
		Vector3 colorVector = new Vector3(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
		Vector3 targetColorVector = new Vector3(Colors[currentIndex].r, Colors[currentIndex].g, Colors[currentIndex].b);
		
		if(Mathf.Abs((targetColorVector - colorVector).magnitude) <= ChangeThreshold)
		{
			spriteRenderer.color = Colors[currentIndex];
			currentIndex++;
			if(currentIndex >= Colors.Length)
				currentIndex = 0;
		}
	}
}
