public class Player
{
    public string playerName;
    public int rank;
    public string role;
    public int health;
    public int score;  // New field for tracking the player's score
    public int experience; // New field for experience points
    
    // Constructor for initializing Player
    public Player(string playerName, int rank, string role, int health = 100, int score = 0, int experience = 0)
    {
        this.playerName = playerName;
        this.rank = rank;
        this.role = role;
        this.health = health;
        this.score = score;
        this.experience = experience;
    }
    
    // Methods for updating player stats
    public void AddScore(int points)
    {
        score += points;
    }

    public void GainExperience(int xp)
    {
        experience += xp;
    }

    // Example method for taking damage (if health is involved)
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }
}
