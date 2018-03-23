using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public static List<Character> team1Char = new List<Character>();
    public static List<Character> team2Char = new List<Character>();
    public static int turnTeam = 1;
    public List<Character> allChar = new List<Character>();
    public static MapManager mapManager;  // set in GameManager

	void Start () {
        allChar.AddRange(GetAllCharacter());
        SortCharacter(allChar);
	}
	
    public Character[] GetAllCharacter()
    {
        Character[] characters = FindObjectsOfType<Character>();
        return characters;
    }

    public static bool HasAllPlay(int team)
    {
        if (team == 1)
        {
            foreach(Character ch in team1Char)
            {
                if(ch.characterState == state.start)
                {
                    return false;
                }
            }
        }
        else
        {
            foreach (Character ch in team2Char)
            {
                if (ch.characterState == state.start)
                {
                    return false;
                }
            }
        }
        ChangeTeamPlay(team);
        return true;
    }

    public bool SortCharacter(List<Character> characters)
    {
        team1Char.Clear();
        team2Char.Clear();
        foreach(Character character in characters)
        {
            if(character.team == 1)
            {
                team1Char.Add(character);
            }
            else
            {
                team2Char.Add(character);
            }
        }
        return team1Char.Count == 0 || team2Char.Count == 0;
    }

    private static void ChangeTeamPlay(int team)
    {
        if (team == 1)
        {
            foreach(Character ch in team2Char)
            {
                ch.characterState = state.start;
            }
        }
        else
        {
            foreach (Character ch in team1Char)
            {
                ch.characterState = state.start;
            }
        }
        turnTeam = turnTeam == 1 ? 2 : 1;
    }
}
