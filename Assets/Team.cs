using UnityEngine;
using System.Collections;

public class Team {
	public Color Color;
	public int ID;
	public int Score;
	
	public Team(int id, Color color)
	{
		ID = id;
		Color = color;
		Score = 0;
	}
}
