using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public Text teamStatusText; // UI Text element to display team balance status
    public Text playerStatusText; // UI Text element to display player information
    public Button balanceButton; // UI Button to trigger team balancing manually
    public GameObject teamPanel; // Panel for displaying the teams (optional)

    private TeamBalancer teamBalancer; // Reference to the TeamBalancer script

    void Start()
    {
        // Get the TeamBalancer component
        teamBalancer = FindObjectOfType<TeamBalancer>();

        // Setup the balance button to trigger team balancing when clicked
        if (balanceButton != null)
        {
            balanceButton.onClick.AddListener(OnBalanceButtonClicked);
        }

        // Initialize the UI with current team information
        UpdateTeamStatus();
    }

    void Update()
    {
        // You can call UpdateTeamStatus regularly to keep the UI in sync with the game state
        UpdateTeamStatus();
    }

    // Method to update team balance information on the UI
    public void UpdateTeamStatus()
    {
        // Update the team balance status text
        if (teamStatusText != null)
        {
            teamStatusText.text = $"Team A: {teamBalancer.teamA.Count} players\n" +
                                  $"Team B: {teamBalancer.teamB.Count} players";
        }

        // Optionally update player information (e.g., rank or role)
        if (playerStatusText != null)
        {
            playerStatusText.text = "Players:\n";
            foreach (var player in teamBalancer.players)
            {
                playerStatusText.text += $"{player.playerName} - Rank: {player.rank} - Role: {player.role}\n";
            }
        }

        // Optional: Update the team panels to show a list of players in each team
        if (teamPanel != null)
        {
            UpdateTeamPanel();
        }
    }

    // Method to update a UI panel showing the players on each team (optional)
    public void UpdateTeamPanel()
    {
        // Clear existing UI elements for teams
        foreach (Transform child in teamPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Create UI elements for Team A players
        foreach (var player in teamBalancer.teamA)
        {
            CreatePlayerUI(player, "Team A");
        }

        // Create UI elements for Team B players
        foreach (var player in teamBalancer.teamB)
        {
            CreatePlayerUI(player, "Team B");
        }
    }

    // Method to create player UI elements and add them to the team panel
    void CreatePlayerUI(Player player, string teamName)
    {
        GameObject playerObject = new GameObject(player.playerName);
        Text playerText = playerObject.AddComponent<Text>();
        playerText.text = $"{player.playerName} - Rank: {player.rank} - Role: {player.role}";

        // Set the position or add UI styling (fonts, colors, etc.)
        playerText.transform.SetParent(teamPanel.transform);
    }

    // Method to manually trigger team balancing when the balance button is clicked
    public void OnBalanceButtonClicked()
    {
        teamBalancer.BalanceTeams();
        UpdateTeamStatus(); // Update UI after balancing teams
    }
}
