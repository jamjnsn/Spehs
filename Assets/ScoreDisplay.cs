using UnityEngine;
using System.Collections;
using System;

public class ScoreDisplay : MonoBehaviour {
	public float Speed = 0f;
	public int Margin = 2;
	public int TeamID = 1;
	
	void Update () {
		GameObject controller = GameObject.FindWithTag(Tags.GameController);
		Team team = controller.GetComponent<TeamManager>().GetTeam(TeamID);
		int displayedScore = Int32.Parse(guiText.text);
		int newScore = team.Score;
		
		if(Speed > 0)
		{
			if(team.Score != displayedScore)
				newScore = Mathf.RoundToInt(Mathf.Lerp(team.Score, displayedScore, Speed * Time.deltaTime));
			if(team.Score - newScore <= Margin) newScore = team.Score;
		}
		
		guiText.text = newScore.ToString();
	}
}
