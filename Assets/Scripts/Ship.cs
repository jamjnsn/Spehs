using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public int MaxHealth = 10;
	public float Speed = 3.0f;
	public GameObject BulletObject;
	public GameObject PowerUp;
	public float LookSpeed = 3.0f;
	public bool Immune = false;
	public float ImmuneTime = 1.0f;
	public int Points = 1;
	public int Health;
	
	public int TeamID = 0;
	public Team Team;
	
	private float powerupTime;
	
	public void Start()
	{
		SetTeam (TeamID);
	}
	
	public void GrantPowerUp(GameObject powerUp, float duration)
	{
		if(powerUp.GetComponent<Bullet>() == null) return;
		PowerUp = powerUp;
		powerupTime = duration;
		Debug.Log(powerupTime);
	}
	
	public GameObject ActiveBullet {
		get {
			return PowerUp == null ? BulletObject : PowerUp;
		}
	}
	
	void Update()
	{
		if(powerupTime > 0)
		{
			powerupTime -= Time.deltaTime;
			if(powerupTime <= 0)
			{
				PowerUp = null;
				powerupTime = 0;
			}
		}
	}
	
	public void SetTeam(int teamID)
	{
		Team = GameObject.FindWithTag(Tags.GameController).GetComponent<TeamManager>().GetTeam(teamID);
		if(Team == null) Debug.Log ("Team " + teamID.ToString() + " not found.");
	}
	
	public void Damage(Team sourceTeam, int amount)
	{
		ChangeHealth(-amount);
		if(Health <= 0)
			SendMessage("ShipDestroyed", sourceTeam == null ? 0 : sourceTeam.ID, SendMessageOptions.DontRequireReceiver);
	}
	
	public bool Alive
	{
		get {
			return Health > 0;
		}
	}
	
	void ShipDestroyed(int sourceTeamID)
	{
		Team team = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<TeamManager>().GetTeam(sourceTeamID);
		if(team != null && team != Team)
			team.Score += Points;
	}
	
	public void ChangeHealth(int amount)
	{
		Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
	}
}
