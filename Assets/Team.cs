using UnityEngine;
using System.Collections;

public class Team : MonoBehaviour
{

	public enum teams
	{
		friendly,
		// player team
		enemy,
		// non-player team
		neutral,
		// neither team (either can attack)
		none
		// no team (can't attack), same state as not having this script attached
	}

	public teams team = teams.none;

	public int GetTeam ()
	{
		return (int)this.team;
	}

	public void SetTeam (teams team)
	{
		this.team = team;
	}

	public bool IsFriendly (Team otherTeam)
	{
		if (team == otherTeam.team || team == teams.none || otherTeam.team == teams.none)
			return true;
		else
			return false;
	}

	public bool IsEnemy (Team otherTeam)
	{
		if (team != otherTeam.team && team != teams.none && otherTeam.team != teams.none)
			return true;
		else
			return false;
	}

	// as stated above, none should be treated the same as not having this script attached
	// if (GetComponent<Team>()) will be false for case of null (no script) AND team == none
	public static implicit operator bool (Team team)
	{
		if (team == null || team.team == teams.none)
			return false;
		else
			return true;
	}
}
