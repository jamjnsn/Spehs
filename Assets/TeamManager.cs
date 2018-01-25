using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour {
	public Color[] Colors;
	private Team[] teams;

	// Use this for initialization
	void Start () {
		List<Team> teams = new List<Team>();
		foreach(Color color in Colors)
			teams.Add (new Team(teams.Count+1, color));
		this.teams = teams.ToArray();
	}
	
	public Team GetTeam(int id)
	{
		foreach(Team team in teams)
			if(team.ID == id) return team;
		return null;
	}
}
