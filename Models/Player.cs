namespace wt_lab7_7postgres_efcore_lyashuk.Models;

public class Player
{
    public int Id { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsActive { get; set; }
    public string Team { get; set; } = "Free Agent";
}