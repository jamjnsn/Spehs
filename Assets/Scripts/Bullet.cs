using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float Speed;
	public float Cooldown;
	Vector2 direction = Vector2.zero;
	float currentSpeed = 0f;
	
	void Update ()
	{
		currentSpeed = Mathf.Lerp(0f, Speed, Time.deltaTime);
		transform.position += direction.ToVector3() * Speed * Time.deltaTime;
	}
	
	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
	}
	
	public void OnBecameInvisible()	
	{
		DestroyObject(gameObject);
	}
}
