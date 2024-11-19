using UnityEngine;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    public List<Player> teamA = new List<Player>();  // List of players in Team A
    public List<Player> teamB = new List<Player>();  // List of players in Team B
    public int teamAScore = 0;  // Score for Team A
    public int teamBScore = 0;  // Score for Team B
    public string winningTeam = "";  // Track the winning team
    public TeamBalancer teamBalancer;  // Reference to the TeamBalancer

    void Start()
    {
        teamBalancer = FindObjectOfType<TeamBalancer>();  // Get reference to the TeamBalancer
        InitializeTeams();
    }

    // Initialize teams with players (useful at the start of the game or after balancing)
    public void InitializeTeams()
    {
        teamA.Clear();
        teamB.Clear();

        foreach (var player in teamBalancer.players)
        {
            if (teamA.Count <= teamB.Count)  // Simple balancing logic (alternating players)
                teamA.Add(player);
            else
                teamB.Add(player);
        }

        // Optionally update the UI after initializing teams
        UIManager.Instance.UpdateTeamStatus();  // Assuming UIManager has a static instance
    }

    // Add player to team A
    public void AddPlayerToTeamA(Player player)
    {
        teamA.Add(player);
        UIManager.Instance.UpdateTeamStatus();  // Update UI when a player joins a team
    }

    // Add player to team B
    public void AddPlayerToTeamB(Player player)
    {
        teamB.Add(player);
        UIManager.Instance.UpdateTeamStatus();  // Update UI when a player joins a team
    }

    // Remove player from a team
    public void RemovePlayerFromTeam(Player player)
    {
        if (teamA.Contains(player))
        {
            teamA.Remove(player);
        }
        else if (teamB.Contains(player))
        {
            teamB.Remove(player);
        }

        UIManager.Instance.UpdateTeamStatus();  // Update UI when a player leaves a team
    }

    // Increment the score for a team
    public void IncrementScore(string team)
    {
        if (team == "A")
        {
            teamAScore++;
        }
        else if (team == "B")
        {
            teamBScore++;
        }

        CheckForWin();  // Check if any team has won after scoring
        UIManager.Instance.UpdateTeamStatus();  // Update UI with the new score
    }

    // Decrement the score for a team (if needed)
    public void DecrementScore(string team)
    {
        if (team == "A" && teamAScore > 0)
        {
            teamAScore--;
        }
        else if (team == "B" && teamBScore > 0)
        {
            teamBScore--;
        }

        UIManager.Instance.UpdateTeamStatus();  // Update UI with the new score
    }

    // Check if a team has won
    public void CheckForWin()
    {
        if (teamAScore >= 10)  // Example win condition (e.g., first to 10 points)
        {
            winningTeam = "Team A";
            EndGame();
        }
        else if (teamBScore >= 10)  // Example win condition (e.g., first to 10 points)
        {
            winningTeam = "Team B";
            EndGame();
        }
    }

    // End the game and display the winner
    public void EndGame()
    {
        // Display the winning team (can also trigger end-game UI screens)
        Debug.Log($"{winningTeam} wins!");
        // You can also stop the game or reset the teams here.
    }

    // Optional: Reset game (teams and scores)
    public void ResetGame()
    {
        teamA.Clear();
        teamB.Clear();
        teamAScore = 0;
        teamBScore = 0;
        winningTeam = "";
        InitializeTeams();  // Reinitialize the teams
    }
}
