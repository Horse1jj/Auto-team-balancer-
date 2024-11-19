using UnityEngine;
using System.Collections.Generic;

public class TeamBalancer : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    public List<Player> teamA = new List<Player>();
    public List<Player> teamB = new List<Player>();
    public float rebalanceInterval = 30f; // Optional, to rebalance periodically

    void Start()
    {
        BalanceTeams(); // Initial team balance
        InvokeRepeating("BalanceTeams", 0f, rebalanceInterval); // Periodic rebalancing
    }

    public void BalanceTeams()
    {
        teamA.Clear();
        teamB.Clear();

        // Sort players by rank (or other criteria)
        players.Sort((p1, p2) => p1.rank.CompareTo(p2.rank));

        // Alternate players between teams
        for (int i = 0; i < players.Count; i++)
        {
            if (i % 2 == 0)
                teamA.Add(players[i]);
            else
                teamB.Add(players[i]);
        }

        // Log the result
        Debug.Log($"Team A: {teamA.Count} players");
        Debug.Log($"Team B: {teamB.Count} players");
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
        BalanceTeams(); // Rebalance teams after player joins
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
        BalanceTeams(); // Rebalance teams after player leaves
    }

    public void SaveTeams()
    {
        string json = JsonUtility.ToJson(players);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }

    public void LoadTeams()
    {
        string json = PlayerPrefs.GetString("PlayerData", "[]");
        players = JsonUtility.FromJson<List<Player>>(json);
        BalanceTeams(); // Rebalance after loading
    }
}

[System.Serializable]
public class Player
{
    public string playerName;
    public int rank;  // Higher rank means more experienced
    public string role;  // Optional, e.g., "Leader", "Tank", etc.
}

