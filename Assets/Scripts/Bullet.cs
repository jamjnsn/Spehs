using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float Speed;
	public float Cooldown;
	public int Damage;
	public Team Team;
	public AudioClip Sound;
	public bool Piercing = false;
	Vector2 direction = Vector2.zero;
	
	void Update ()
	{
		transform.position += direction.ToVector3() * Speed * Time.deltaTime;
		
		Ray ray = new Ray (transform.position, (transform.rotation * Vector3.up));
		Debug.DrawRay (ray.origin, ray.direction, Color.red);
		RaycastHit2D[] hits = Physics2D.RaycastAll (ray.origin.ToVector2 (), ray.direction.ToVector2 (), 0.0f);
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.gameObject == this.gameObject)
				continue;
			
			Ship ship = hit.collider.gameObject.GetComponent<Ship>();
			
			if(ship != null && ship.Team != Team)
			{
				ShipMotor motor = hit.collider.gameObject.GetComponent<ShipMotor>();
				if(motor.Destroyed) break;
				motor.Damage(Team, Damage);
				if(!Piercing) Destroy (this.gameObject);
			}
		}
	}
	
	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
		SendMessage("BulletDirectionSet", SendMessageOptions.DontRequireReceiver);
	}
	
	public void OnBecameInvisible()	
	{
		DestroyObject(gameObject);
	}
}
