using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ship))]
public class ShipMotor : MonoBehaviour {
	public GameObject ExplosionPrefab;
	public bool Destroyed = false;
	public AudioClip HitSound, DestroySound;
	
	Ship ship;
	float immuneTimer = 0f;
	float weaponCooldown = 0f;
	AudioSource audioSource = null;
		
	void Start()
	{
		ship = GetComponent<Ship>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		weaponCooldown = Mathf.Max(0f, weaponCooldown - Time.deltaTime);
		immuneTimer = Mathf.Max(0f, immuneTimer - Time.deltaTime);
	}
	
	public void Fire(Vector2 direction)
	{
		if(weaponCooldown > 0f || ship.BulletObject == null) return;
		
		GameObject bulletObject = Instantiate(ship.ActiveBullet) as GameObject;
		
		Vector3 bulletPosition = transform.position + transform.forward;
		bulletPosition.z = 0.1f;
		bulletObject.transform.position = bulletPosition;
		bulletObject.transform.rotation = transform.rotation;
		
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.Team = ship.Team;
		bullet.SetDirection(transform.rotation * Vector2.up);
		
		weaponCooldown = bullet.Cooldown;
		
		AudioSource.PlayClipAtPoint(bullet.Sound, transform.position);
	}
	
	public void LookAt(Vector3 target)
	{
		Vector3 dir = target - transform.position;
		dir.z = 0;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward),
			ship.LookSpeed * Time.deltaTime);
	}
	
	public void Move(Vector2 direction)
	{
		if(audioSource != null)
		{
			if(direction != Vector2.zero)
				if(!audioSource.isPlaying) audioSource.Play();
			else
				audioSource.Stop ();
		}
		
		SetFlames(direction != Vector2.zero);
		
		transform.position = Vector3.Lerp(transform.position, transform.position + direction.ToVector3(), Time.deltaTime * ship.Speed);
		
		CheckCrash ();
	}
	
	public void MoveForward()
	{
		Move ((transform.rotation * Vector3.forward).ToVector2());
	}

	void CheckCrash ()
	{
		Ray ray = new Ray (transform.position, (transform.rotation * Vector3.up));
		Debug.DrawRay (ray.origin, ray.direction, Color.red);
		RaycastHit2D[] hits = Physics2D.RaycastAll (ray.origin.ToVector2 (), ray.direction.ToVector2 (), 1.0f);
		
		foreach (RaycastHit2D hit in hits) {
			if (hit.collider.gameObject == this.gameObject)
				continue;
			if (this.tag == Tags.Player && hit.collider.tag == Tags.Enemy)
				Debug.Log ("Crash");
		}
	}
	
	public void Damage(Team sourceTeam, int amount)
	{
		ship.Damage(sourceTeam, amount);
		AudioSource.PlayClipAtPoint(HitSound, transform.position);
	}
	
	void ShipDestroyed()
	{
		AudioSource.PlayClipAtPoint(DestroySound, transform.position);
		GameObject explosion = Instantiate (ExplosionPrefab) as GameObject;
		explosion.transform.position = transform.position;
		explosion.transform.parent = transform;
		explosion.GetComponent<Explosion>().ShipObject = this.gameObject;
		Destroyed = true;
	}
	
	public void SetFlames(bool on)
	{
		foreach(Transform child in transform)
			if(child.name == "Flame")
				child.gameObject.SetActive(on);
	}
}
