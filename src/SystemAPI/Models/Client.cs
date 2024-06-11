namespace SystemAPI.Models;

public class Client
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age { get; set; }    
}