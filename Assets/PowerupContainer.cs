using UnityEngine;
using System.Collections;

public class PowerupContainer : MonoBehaviour {
	public GameObject BulletPrefab;
	public float Duration;
	
	void Update()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position.ToVector2(), Vector2.zero);
		
		foreach(RaycastHit2D hit in hits)
		{
			Ship ship = hit.collider.gameObject.GetComponent<Ship>();
			if(ship != null && ship.Alive)
			{
				ship.GrantPowerUp(BulletPrefab, Duration);
				Destroy (this.gameObject);
			}
		}
	}
}
