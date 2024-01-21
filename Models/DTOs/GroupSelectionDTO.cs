namespace config.Models.DTOs;

public class GroupSelectionDTO
{
    public string Name { get; set; }

    public List<string> Options { get; set; } = new List<string>();
}
