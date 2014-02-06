using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ship))]
public class ShipMotor : MonoBehaviour {
	public int MaxHealth = 10;
	public float Speed = 3.0f;
	public GameObject BulletObject;
	public float LookSpeed = 3.0f;
	public bool Immune = false;
	public float ImmuneTime = 1.0f;
	
	protected int health;
	protected float immuneTimer = 0f;
	protected float weaponCooldown = 0f;
	
	Ship ship;
	
	void Start()
	{
		ship = GetComponent<Ship>();
	}
	
	void Update () {
		weaponCooldown = Mathf.Max(0f, weaponCooldown - Time.deltaTime);
		immuneTimer = Mathf.Max(0f, immuneTimer - Time.deltaTime);
	}
	
	public void Fire(Vector2 direction)
	{
		if(weaponCooldown > 0f || BulletObject == null) return;
		
		GameObject bulletObject = Instantiate(BulletObject) as GameObject;
		
		Vector3 bulletPosition = transform.position + transform.forward;
		bulletPosition.z = 0.1f;
		bulletObject.transform.position = bulletPosition;
		bulletObject.transform.rotation = transform.rotation;
		
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.SetDirection(direction);
		
		weaponCooldown = bullet.Cooldown;
	}
	
	public void LookAt(Vector3 target)
	{
		Vector3 dir = target - transform.position;
		dir.z = 0;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward),
			LookSpeed * Time.deltaTime);
	}
	
	public void Move(Vector2 direction)
	{
		SetFlames(direction != Vector2.zero);
		transform.position = Vector3.Lerp(transform.position, transform.position + direction.ToVector3() * Speed, Time.deltaTime);
		
		Ray ray = new Ray(transform.position, (transform.rotation * Vector3.up));
		Debug.DrawRay(ray.origin, ray.direction, Color.red);
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin.ToVector2(), ray.direction.ToVector2(), 1.0f);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.collider.gameObject == this.gameObject) continue;
			if(this.tag == Tags.Player && hit.collider.tag == Tags.Enemy)
				Debug.Log ("Crash");
		}
	}
	
	public void Damage(int amount)
	{
		
	}
	
	public void SetFlames(bool on)
	{
		foreach(Transform child in transform)
			if(child.name == "Flame")
				child.gameObject.SetActive(on);
	}
}
